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
                //item.Text = info.CompetingType.ToString();
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
    }
}