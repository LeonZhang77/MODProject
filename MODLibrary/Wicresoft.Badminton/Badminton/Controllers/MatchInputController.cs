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
            model.TrueActiveChampionshipList = GetTrueActiveChampionshipList();
            model.TipMessage = this.TempData["message"] as String;
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

        public List<SelectListItem> GetTrueActiveChampionshipList() 
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<ChampionshipInfo> infoList = provider.GetChampionshipInfos().ToList();
            SelectListItem item;
            foreach (ChampionshipInfo info in infoList)
            {
                if (info.IsActive)
                {
                    item = new SelectListItem();
                    item.Text = info.Title;
                    item.Value = info.ID.ToString();
                    returnList.Add(item);
                }
               
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
        [HttpPost]
        public ActionResult AddMatch(MatchInputModel model)
        {
            
            try
            {
                MatchInfo info = new MatchInfo();
                info.ChampionID = provider.GetChampionshipInfoByID(Convert.ToInt32(model.ChampionshipID));
                info.InputPersonID = provider.GetMemberInfoByID(Convert.ToInt32(model.InputPersonID));
                info.MatchDate = DateTime.Parse(model.MatchDate);
                info.WinnerID = provider.GetMemberInfoByID(Convert.ToInt32(model.Winner1ID));
                info.LoserID = provider.GetMemberInfoByID(Convert.ToInt32(model.Loser1ID));
                info.LoserPoints = int.Parse(model.LoserPoints);
                info.WinnerPoints = int.Parse(model.WinnerPoints);
                info.Compensation = 0;
                info.Updated = false;
                info.Verified = false;
                info.VerifyDate = DateTime.Parse("01/01/1900");
                if (info.ChampionID.CompetingType.Equals(CompetingType.MaleDou) ||
                    info.ChampionID.CompetingType.Equals(CompetingType.FemaleDou) ||
                    info.ChampionID.CompetingType.Equals(CompetingType.MixDou))
                {
                    info.WinnerID2 = provider.GetMemberInfoByID(Convert.ToInt32(model.Winner2ID));
                    info.LoserID2 = provider.GetMemberInfoByID(Convert.ToInt32(model.Loser2ID));
                }
                info.CreateTime = DateTime.Now;
                provider.SaveMatchInfo(info);
            }
            catch (Exception ex) {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddFail);
                return RedirectToAction("Index", "MatchInput");
            };
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddSucc);
            return RedirectToAction("Index", "MatchInput");
        }

        
    }
}
