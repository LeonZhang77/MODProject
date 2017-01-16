using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.MatchInput;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Newtonsoft.Json.Linq;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Controllers
{
    public class MatchInputController : Controller
    {
        IBadmintionDataProvider provider;

        public MatchInputController()
        {
            this.provider = new BadmintionDataProvider();
        }
        public ActionResult Index()
        {
            MatchInputModel model = new MatchInputModel();
            model.ChampionshipList = GetChampionshipList();
            model.ChampionshipCompetingTypeList = GetChampionshipCompetingTypeList();
            model.CompetingList = EnumHelper.GetEnumIEnumerable(CompetingType.FemaleDou);
            model.SearchMemberList = GetSearchMemberList();
            model.SearchMaleMemberList = GetSearchMemberList(true);
            model.SearchFemaleMemberList = GetSearchMemberList(false);                
            return View(model);
        }

        internal List<SelectListItem> GetChampionshipList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<ChampionshipInfo> infoList = provider.GetChampionshipInfos().ToList();
            SelectListItem item;
            foreach (ChampionshipInfo info in infoList)
            {
                item = new SelectListItem();
                item.Text = info.Title;
                item.Value = info.ID.ToString();
                returnList.Add(item);
            }
            return returnList;
        }

        internal List<SelectListItem> GetChampionshipCompetingTypeList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<ChampionshipInfo> infoList = provider.GetChampionshipInfos().ToList();
            SelectListItem item;
            foreach (ChampionshipInfo info in infoList)
            {
                item = new SelectListItem();
                item.Text = EnumHelper.GetEnumDescription(info.CompetingType);
                item.Value = info.ID.ToString();
                returnList.Add(item);
            }
            return returnList;
        }

        internal List<SelectListItem> GetSearchMemberList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<MemberInfo> infoList = provider.GetMemberInfos().ToList();
            SelectListItem item;
            foreach (MemberInfo info in infoList)
            {
                item = new SelectListItem();
                item.Text = info.Name;
                item.Value = info.ID.ToString();
                returnList.Add(item);
            }
            return returnList;
        }

        internal List<SelectListItem> GetSearchMemberList(bool Male)
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<MemberInfo> infoList = provider.GetMemberInfos().ToList();
            SelectListItem item;
            foreach (MemberInfo info in infoList)
            {
                if (info.Male.Equals(Male))
                {
                    item = new SelectListItem();
                    item.Text = info.Name;
                    item.Value = info.ID.ToString();
                    returnList.Add(item);
                }            
            }
            return returnList;
        }

        public string AddMatch(string q)
        {
            try
            {
                JObject obj = JObject.Parse(q);
                
                MatchInfo info = new MatchInfo();
                info.ChampionID = provider.GetChampionshipInfoByID(long.Parse((string)obj["Championship"]));
                info.CreateTime = DateTime.Now;
                info.LoserID = provider.GetMemberInfoByID(long.Parse((string)obj["Loser1ID"]));
                info.LoserPoints = Int32.Parse((string)obj["LoserPoints"]);
                info.MatchDate = DateTime.Parse((string)obj["MatchDate"]);
                info.WinnerID = provider.GetMemberInfoByID(long.Parse((string)obj["Winner1ID"]));
                info.WinnerPoints = Int32.Parse((string)obj["WinnerPoints"]);
                info.Compensation = 0;
                info.Updated = false;
                info.InputPersonID = provider.GetMemberInfoByID(long.Parse((string)obj["InputPersonID"]));
                info.VerifyDate = DateTime.Parse("01/01/1900");
                info.Verified = false;
                
                if (!EnumHelper.GetEnumDescription((info.ChampionID.CompetingType)).Contains("Singles"))
                {
                    info.WinnerID2 = provider.GetMemberInfoByID(long.Parse((string)obj["Winner2ID"]));
                    info.LoserID2 = provider.GetMemberInfoByID(long.Parse((string)obj["Loser2ID"]));
                }
                provider.SaveMatchInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }
    }
}
