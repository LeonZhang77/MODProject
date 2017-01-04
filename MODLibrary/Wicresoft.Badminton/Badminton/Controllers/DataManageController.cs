using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.DataManage;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;

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
            return View(model);
        }

        public List<MemberModel> GetMemberList()
        {
            List<MemberModel> returnList = new List<MemberModel>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            MemberModel model;
            foreach (MemberInfo item in memberList)
            {
                model = new MemberModel();
                model.ID = item.ID;
                model.Name = item.Name;
                model.Male = item.Male;
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
                model = new ClubModel();
                model.ID = item.ID;
                model.Name = item.Name;
                model.Description = item.Description;
                returnList.Add(model);
            }
            return returnList;
        }
    }
}