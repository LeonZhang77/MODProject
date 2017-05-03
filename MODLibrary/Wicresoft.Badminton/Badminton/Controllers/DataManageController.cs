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
            model.MemberWithoutClub = GetMemberListByWithoutClub();
            model.TipMessage = this.TempData["message"] as String;
            return View(model);
        }

        public List<ChampionshipModel> GetChampionShipList()
        {
            List<ChampionshipModel> returnList = new List<ChampionshipModel>();
            List<ChampionshipInfo> championshipList = provider.GetChampionshipInfos().ToList();
            ChampionshipModel model;
            foreach(ChampionshipInfo item in championshipList)
            {
                model = ChampionshipModel.GetViewModel(item, provider);
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
                model = MemberModel.GetViewModel(item, provider);
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
        public List<MemberAndClubRelationsModel> GetMemberAndClubRelationListByClub(int ClubID)
        {

            List<MemberAndClubRelationsModel> returnList = new List<MemberAndClubRelationsModel>();
            ClubInfo club = provider.GetClubInfoByID(ClubID);
            List<MemberAndClubRelation> relationList = provider.GetMemberAndClubRelations(club).ToList();

            MemberAndClubRelationsModel model;


            foreach (MemberAndClubRelation item in relationList)
            {
                model = MemberAndClubRelationsModel.GetViewModel(item);
                returnList.Add(model);
            }
            return returnList;
        }
        public JsonResult GetMemberAndClubRelationListToJsonByClub(int clubID)
        {
            List<MemberAndClubRelationsModel> modelList = GetMemberAndClubRelationListByClub(clubID);
            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetMemberListByWithoutClub()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            SelectListItem item = new SelectListItem();
            returnList.Add(item);
            foreach (MemberInfo model in memberList)
            {
                if (provider.GetMemberAndClubRelations(model).Count() <= 0)
                {
                    item = new SelectListItem();
                    item.Value = model.ID.ToString();
                    item.Text = model.Name;
                    returnList.Add(item);
                }
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
            catch (Exception ex) {
                return ex.Message; 
            };
            return "true";
        }

        public string DeleteMember(long q)
        {
            try
            {
                MemberInfo memberInfo = provider.GetMemberInfoByID(q);
                provider.DeleteMemberInfo(memberInfo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            };
            return "true";
        }

        public string DeleteChampionship(long q)
        {
            try
            {
                ChampionshipInfo info = provider.GetChampionshipInfoByID(q);
                provider.DeleteChampionshipInfo(info);
            }
            catch (Exception ex)
            {
                return ex.Message;
            };
            return "true";
        }


        public ActionResult AddMember(MemberModel model)
        {
            try
            {

                MemberInfo info = new MemberInfo();
                info.Name = model.Name;
                info.Male = model.Male;
                info.CreateTime = DateTime.Now;
                info.UpdateDate = info.CreateTime;

                provider.SaveMemberInfo(info);

                if (model.ClubID > 0)
                {
                    MemberInfo forMemberAndClubMemberInfo = provider.GetMemberInfoByName(model.Name);
                    ClubInfo forMemberAndClubClubInfo = provider.GetClubInfoByID(model.ClubID);

                    MemberAndClubRelation memberAndClubInfo = new MemberAndClubRelation();
                    memberAndClubInfo.ClubID = forMemberAndClubClubInfo;
                    memberAndClubInfo.MemberID = forMemberAndClubMemberInfo;
                    memberAndClubInfo.IsCaption = false;
                    memberAndClubInfo.CreateTime = DateTime.Now;

                    provider.SaveMemberAndClubRelation(memberAndClubInfo);
                }
            }
            catch (Exception ex) 
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddFail);
                return RedirectToAction("Index", "DataManage"); 
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddSucc);
            return RedirectToAction("Index", "DataManage");
        }

        public ActionResult AddChampionship(ChampionshipModel model)
        {
            try
            {
               
                ChampionshipInfo info = new ChampionshipInfo();
                info.Title =model.Title;
                info.StartDate = DateTime.Parse(model.StartDate);
                info.EndDate = DateTime.Parse(model.EndDate);
                info.ChampionType = (ChampionType)Enum.ToObject(typeof(ChampionType), model.ChampionTypeID);
                info.CompetingType = (CompetingType)Enum.ToObject(typeof(CompetingType), model.CompetingTypeID);

                provider.SaveChampionshipInfo(info);
            }
            catch (Exception ex)
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddFail);
                return RedirectToAction("Index", "DataManage");
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddSucc);
            return RedirectToAction("Index", "DataManage");
        }

        public ActionResult AddClub(ClubModel model)
        {
            try
            {
                ClubInfo info = new ClubInfo();
                info.CreateTime = DateTime.Now;
                info.Name = model.Name;
                info.Description = model.Description;

                provider.SaveClubInfo(info);

                if (model.CaptainID > 0)
                {
                    MemberAndClubRelation relationInfo = new MemberAndClubRelation();
                    relationInfo.MemberID = provider.GetMemberInfoByID((long)model.CaptainID);
                    relationInfo.ClubID = provider.GetClubInfoByName(model.Name);
                    relationInfo.IsCaption = true;
                    relationInfo.CreateTime = DateTime.Now;
                    provider.SaveMemberAndClubRelation(relationInfo);
                }

            }
            catch (Exception ex) 
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddFail);
                return RedirectToAction("Index", "DataManage");
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddSucc);
            return RedirectToAction("Index", "DataManage");
        }

        public ActionResult EditMember(MemberModel model)
        {
            try
            {
               
                MemberInfo info = provider.GetMemberInfoByID(model.ID);
                info.Name = model.Name;
                info.Male = model.Male;
                info.UpdateDate = DateTime.Now;

                provider.UpdateMemberInfo(info);

                var memberAndClubInfo = provider.GetMemberAndClubRelations(info);
                //execute delete relation for condition that admin update the member's club to null, but member have a club.
                if (memberAndClubInfo.ToList().Count > 0 && string.IsNullOrEmpty(model.ClubName))
                {
                    provider.DeleteMemberAndClubRelation(memberAndClubInfo.ToList().FirstOrDefault());
                }
                //execute update ralation for condition that admin change the member's club.
                if (memberAndClubInfo.ToList().Count > 0 && !string.IsNullOrEmpty(model.ClubName))
                {
                    MemberAndClubRelation updateRelation = memberAndClubInfo.FirstOrDefault();
                    updateRelation.ClubID = provider.GetClubInfoByName(model.ClubName);
                    updateRelation.IsCaption = false;
                    provider.UpdateMemberAndClubRelation(updateRelation);
                }
                //execute add relation for condition that member haven't a club before.
                if (memberAndClubInfo.ToList().Count <= 0 && !string.IsNullOrEmpty(model.ClubName))
                {
                    MemberAndClubRelation addRelation = new MemberAndClubRelation();
                    addRelation.ClubID = provider.GetClubInfoByName(model.ClubName);
                    addRelation.MemberID = info;
                    addRelation.IsCaption = false;
                    provider.SaveMemberAndClubRelation(addRelation);
                }

            }
            catch (Exception ex) 
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditFail);
                return RedirectToAction("Index", "DataManage");
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditSucc);
            return RedirectToAction("Index", "DataManage");
        }

        public ActionResult EditChampionship(ChampionshipModel model)
        {
            try
            {
                ChampionshipInfo info = provider.GetChampionshipInfoByID(model.ID);
                info.Title = model.Title;
                info.StartDate = DateTime.Parse(model.StartDate);
                info.EndDate = DateTime.Parse(model.EndDate);
                info.ChampionType = (ChampionType)Enum.ToObject(typeof(ChampionType), model.ChampionTypeID);
                info.CompetingType = (CompetingType)Enum.ToObject(typeof(CompetingType), model.CompetingTypeID);

                provider.UpdateChampionshipInfo(info);
                
            }
            catch (Exception ex) 
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditFail);
                return RedirectToAction("Index", "DataManage");
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditSucc);
            return RedirectToAction("Index", "DataManage");
        }


        public ActionResult EditClub(ClubModel model) 
        {
            try 
            {
                ClubInfo info = provider.GetClubInfoByID((long)model.ID);
                info.Name = model.Name;
                info.Description = model.Description;

                provider.UpdateClubInfo(info);

                MemberAndClubRelation relation;
                if (model.CaptainID > 0)
                {
                    relation = provider.GetMemberAndClubRelationByID(model.CaptainID);
                    if (!relation.IsCaption)
                    {
                        List<MemberAndClubRelation> relationList = provider.GetMemberAndClubRelations(info).ToList();
                        foreach (MemberAndClubRelation item in relationList)
                        {
                            if (item.IsCaption)
                            {
                                item.IsCaption = false;
                                provider.UpdateMemberAndClubRelation(item);
                            }
                        }

                    }
                    relation.IsCaption = true;
                    provider.UpdateMemberAndClubRelation(relation);
                }
                else 
                {
                    List<MemberAndClubRelation> relationList = provider.GetMemberAndClubRelations(info).ToList();
                    foreach (MemberAndClubRelation item in relationList)
                    {
                        if (item.IsCaption)
                        {
                            item.IsCaption = false;
                            provider.UpdateMemberAndClubRelation(item);
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                this.TempData["message"]=EnumHelper.GetEnumDescription(MessagePoint.EditFail);
                return RedirectToAction("Index", "DataManage");
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditSucc);
            return RedirectToAction("Index", "DataManage");
         
        }

        public string BatchAddMemberToClub(int ClubID,string[] Members) 
        {
            try {
                ClubInfo info = provider.GetClubInfoByID(ClubID);
                MemberInfo memberInfo = null;
                List<MemberAndClubRelation> relationList = new List<MemberAndClubRelation>();
                foreach (string member in Members)
                {
                    memberInfo = provider.GetMemberInfoByID(int.Parse(member));
                    MemberAndClubRelation relationInfo = new MemberAndClubRelation();
                    relationInfo.ClubID = info;
                    relationInfo.MemberID = memberInfo;
                    relationInfo.IsCaption = false;
                    relationInfo.CreateTime = DateTime.Now;
                    relationList.Add(relationInfo);
                }
                provider.BatchAddMemberAndClubRelation(relationList);
            }
            catch (Exception ex) {
                return ex.Message;
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.AddSucc);
            return "true";
        }

        public string BatchRemoveMemberFromClub(string[] Relations)
        {
            try
            {
                List<MemberAndClubRelation> relationList = new List<MemberAndClubRelation>();
                //string[] relationArr = Relations.Split(',');
                foreach (string relation in Relations) 
                {
                    MemberAndClubRelation relationInfo = provider.GetMemberAndClubRelationByID(int.Parse(relation));
                    relationList.Add(relationInfo);
                }
                provider.BatchDeleteMemberAndClubRelation(relationList);
                
            }
            catch (Exception ex) {
                return ex.Message; 
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.DelSucc);
            return "true";
        }

        public string SetChampionToActive(int ChampionID,bool IsActive)
        {
            try {
                ChampionshipInfo info = provider.GetChampionshipInfoByID(ChampionID);
                info.IsActive = IsActive;

                provider.UpdateChampionshipInfo(info);
            }
            catch (Exception ex) {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditFail); 
                return ex.Message; 
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditSucc); 
            return "true";
        } 
        
        public JsonResult CheckNameRepeat(int ID,string Name)
        {
            if (ID == -1)
            {
                if (provider.GetMemberInfoByName(Name) != null)
                {
                    return Json(false,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<MemberInfo> list = provider.GetMemberInfos().ToList();
                var isExist = from m in list where m.ID!=ID && m.Name==Name select m.Name;
                if (isExist.ToList().Count > 0) 
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckClubNameRepeat(int ID,string Name)
        {
            if (ID == -1)
            {
                if (provider.GetClubInfoByName(Name) != null)
                {
                    return Json(false,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<ClubInfo> list = provider.GetClubInfoInfos().ToList();
                var isExist = from m in list where m.ID!=ID && m.Name==Name select m.Name;
                if (isExist.ToList().Count > 0) 
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckChampionNameRepeat(int ID, string Name)
        {
            if (ID == -1)
            {
                if (provider.GetChampionshipInfoByName(Name) != null)
                {
                    return Json(false,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<ChampionshipInfo> list = provider.GetChampionshipInfos().ToList();
                var isExist = from m in list where m.ID != ID && m.Title == Name select m.Title;
                if (isExist.ToList().Count > 0) 
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        } 
    }

}
