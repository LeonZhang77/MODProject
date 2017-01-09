using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.DataManage;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Newtonsoft.Json.Linq;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Controllers
{
    public class DataManageController : Controller
    {
        IBadmintionDataProvider provider;

        public DataManageController()
        {
            provider = new BadmintionDataProvider();
        }
        public ActionResult Index()
        {
            DataManageIndexModel model = new DataManageIndexModel();
            model.ClubList = GetClubList();
            model.MemberList = GetMemberList();
            model.ChampionshipList = GetChampionShipList();
            model.ChampionshipTypeSelectList = EnumHelper.GetEnumIEnumerable<ChampionType>(ChampionType.Normal);
            model.CompetingTypeSelectList = EnumHelper.GetEnumIEnumerable<CompetingType>(CompetingType.MaleSin);
            return View(model);
        }

        public List<ChampionshipModel> GetChampionShipList()
        {
            List<ChampionshipModel> returnList = new List<ChampionshipModel>();
            List<ChampionshipInfo> championshipList = provider.GetChampionshipInfos().ToList();
            ChampionshipModel model;
            foreach(ChampionshipInfo item in championshipList)
            {
                model = ChampionshipModel.GetViewModel(item);
                returnList.Add(model);
            }
            return returnList;
        }

        public List<MemberModel> GetMemberList()
        {
            List<MemberModel> returnList = new List<MemberModel>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            MemberModel model;
            foreach (MemberInfo item in memberList)
            {
                model = MemberModel.GetViewModel(item);
                returnList.Add(model);
            }
            return returnList;

        }

        public List<ClubModel> GetClubList()
        {
            List<ClubModel> returnList = new List<ClubModel>();
            List<ClubInfo> clubList = provider.GetClubInfoInfos().ToList();
            ClubModel model;
            foreach (ClubInfo item in clubList)
            {
                model = ClubModel.GetViewModel(item);                
                returnList.Add(model);
            }
            return returnList;
        }

        public string DeleteClub(long q)
        {
            try
            {
                ClubInfo clubInfo = provider.GetClubInfoByID(q);
                provider.DeleteClubInfo(clubInfo);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }

        public string DeleteChampionship(long q)
        {
            try
            {
                ChampionshipInfo info = provider.GetChampionshipInfoByID(q);
                provider.DeleteChampionshipInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }

        public string AddChampionship(string q)
        {
            try
            {
                JObject obj = JObject.Parse(q);
                string title = (string)obj["title"];
                DateTime startDate =DateTime.Parse((string)obj["stratDate"]);
                DateTime endDate = DateTime.Parse((string)obj["endDate"]);
                int Championtype = int.Parse((string)obj["Championtype"]);
                int Competingtype = int.Parse((string)obj["Competingtype"]);

                ChampionshipInfo info = new ChampionshipInfo();
                info.Title = title;
                info.StartDate = startDate;
                info.EndDate = endDate;
                info.ChampionType = (ChampionType)Enum.ToObject(typeof(ChampionType), Championtype);
                info.CompetingType = (CompetingType)Enum.ToObject(typeof(CompetingType), Competingtype);

                provider.SaveChampionshipInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }
    }
}