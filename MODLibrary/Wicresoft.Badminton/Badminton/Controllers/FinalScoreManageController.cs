using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Badminton.Models.FinalScoreManage;

namespace Badminton.Controllers
{
    public class FinalScoreManageController : Controller
    {

        IBadmintionDataProvider provider;

        public FinalScoreManageController()
        {
            provider = new BadmintionDataProvider();
            
        }
        // GET: FinalScoreManage
        public ActionResult Index()
        {
            FinalScoreManageIndexModel model = new FinalScoreManageIndexModel();
            model.Parameters.ChampionshipList = GetChampionshipList();
            model.Parameters.SearchMemberList = GetSearchMemberList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FinalScoreManageIndexModel modelInput)
        {
            ActionResult returnAction = new ViewResult();
            modelInput.ErrorState = false;

            switch (modelInput.Parameters.ActionSteps)
            {
                case 1:
                    modelInput = AddToBonusList(modelInput);
                    break;
                case 2:
                    modelInput = ReviewScoreListAndMemberList(modelInput);
                    break;
                case 3:
                    modelInput = SaveBonusAndScoreEntry(modelInput);
                    break;               
                default:
                    modelInput = new FinalScoreManageIndexModel();
                    break;

            }

            return View("Index", modelInput);
        }

        public FinalScoreManageIndexModel AddToBonusList(FinalScoreManageIndexModel modelInput)
        {
            FinalScoreBonusInfo bonusInfo = new FinalScoreBonusInfo();

            bonusInfo.CreateTime = DateTime.Now;
            bonusInfo.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
            bonusInfo.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
            bonusInfo.MemberID = long.Parse(modelInput.Parameters.MemberID);
            bonusInfo.MembernName = provider.GetMemberInfoByID(bonusInfo.MemberID).Name;
            bonusInfo.ChampionID = long.Parse(modelInput.Parameters.ChampionshipID);
            bonusInfo.ChampionTitle = provider.GetChampionshipInfoByID(bonusInfo.ChampionID).Title;
            bonusInfo.Score = modelInput.Parameters.Score;            
            
            modelInput.FinalScoreBonusList.Add(bonusInfo);
            modelInput.Parameters.ActionSteps = 0;            
            return modelInput;
        }

        public FinalScoreManageIndexModel ReviewScoreListAndMemberList(FinalScoreManageIndexModel modelInput)
        {
            return modelInput;
        }
        public FinalScoreManageIndexModel SaveBonusAndScoreEntry(FinalScoreManageIndexModel modelInput)
        {
            if (modelInput.FinalScoreBonusList.Count == 0 && modelInput.AddScoreInfoList.Count == 0)
            {
                modelInput.StateMessage = "没有需要保存的Score和Bonus, 请先计算并检查！";
                modelInput.ErrorState = true;
                return modelInput;
            }

            Boolean flag = RecordScoreEntryToDB(modelInput);
            if (flag)
            {
                modelInput = new FinalScoreManageIndexModel();
                modelInput.StateMessage = "保存成功, 请记得去积分计算页面按周期调整积分！";
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput.StateMessage = "保存没有成功！";
                modelInput.ErrorState = true;
            }

            return modelInput;
        }

        public Boolean RecordScoreEntryToDB(FinalScoreManageIndexModel modelInput)
        {

            try
            {
                SaveBonusInfors(modelInput.FinalScoreBonusList);
                SaveScoreinfors(modelInput.AddScoreInfoList);
            }
            catch (Exception ex) { return false; };

            return true;
        }

        internal void SaveBonusInfors(List<FinalScoreBonusInfo> AddBonusInfoList)
        {
            foreach (FinalScoreBonusInfo item in AddBonusInfoList)
            {
                BonusInfo info = FinalScoreBonusInfo.GetEntity(item);
                provider.SaveBonusInfo(info);                
            }
        }

        internal void SaveScoreinfors(List<AddScoreInfo> AddScoreInfoList)
        {
            foreach (AddScoreInfo item in AddScoreInfoList)
            {
                ScoreInfo info = AddScoreInfo.GetEntity(item);
                provider.SaveScoreInfo(info);
            }
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
    }
}