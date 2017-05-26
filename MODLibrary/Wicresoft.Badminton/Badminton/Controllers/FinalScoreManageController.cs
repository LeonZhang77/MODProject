using System;
using System.Text;
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
        static string stateMessage_ThereIsNoNewBonusInfos = "你并没有增加新的Bonus记录!";
        static string stateMessage_NoScoreOrBonusNeedToBeSaved = "没有需要保存的Score或Bonus, 请先计算并检查！";
        static string stateMessage_SavedAndGoToScoreCalcPageAndAdjust = "保存成功, 请记得去积分计算页面按周期调整积分！";
        static string stateMessage_SaveNotSuccess = "保存没有成功！";

        IBadmintionDataProvider provider;
        List<DataHelper.MemberRank> rankList;

        public FinalScoreManageController()
        {
            provider = new BadmintionDataProvider();
            rankList = DataHelper.GetMemberRank(provider.GetMemberInfos().ToList());
            
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
            
            modelInput.ErrorState = false;

            switch (modelInput.Parameters.ActionSteps)
            {
                case 1:
                    modelInput = AddToBonusList(modelInput);
                    break;
                case 2:
                    modelInput = ReviewScoreList(modelInput);
                    break;
                case 3:
                    modelInput = AdjustAccordingToDateRange(modelInput);
                    break;
                case 4:
                    modelInput = SaveBonusAndScoreEntry(modelInput);
                    break;
                case 5:
                    modelInput = DeleteBonusInfo(modelInput);
                    break;  
                default:
                    modelInput = new FinalScoreManageIndexModel();
                    break;

            }

            modelInput.Parameters.ChampionshipList = GetChampionshipList();
            modelInput.Parameters.SearchMemberList = GetSearchMemberList();
            return View("Index", modelInput);
        }

        public FinalScoreManageIndexModel DeleteBonusInfo(FinalScoreManageIndexModel modelInput)
        {
            modelInput.FinalScoreBonusList.RemoveAt((int)modelInput.Parameters.Score);
            return modelInput;
        }

        public FinalScoreManageIndexModel AdjustAccordingToDateRange(FinalScoreManageIndexModel modelInput)
        {
            if (modelInput.AddScoreInfoList.Count == 0 && modelInput.FinalScoreBonusList.Count != 0)
            {
                modelInput.StateMessage = stateMessage_ThereIsNoNewBonusInfos;
                modelInput.ErrorState = true;
            }
            else
            {
                List<ScoreInfo> scoreInfoList = provider.GetScoreInfos().ToList();
                ScoreInfo tempInfo;
                foreach (AddScoreInfo item in modelInput.AddScoreInfoList)
                {
                    tempInfo = AddScoreInfo.GetEntity(item);
                    scoreInfoList.Add(tempInfo);
                }
                modelInput.UpdateMembersList = GetUpdateMemberList(scoreInfoList, modelInput);

            }
            return modelInput;
        }

        internal List<UpdateMembers> GetUpdateMemberList(List<ScoreInfo> ScoreInfoList, FinalScoreManageIndexModel modelInput)
        {
            UpdateMembers updateMember;
            List<MemberInfo> memberInfoList = provider.GetMemberInfos().ToList();
            FinalScoreManageIndexModel model = modelInput;
            //初始化 model.UpdateMemberList.
            foreach (MemberInfo item in memberInfoList)
            {
                updateMember = UpdateMembers.GetModel(item, rankList);
                model.UpdateMembersList.Add(updateMember);
            }

            // 更新 update Score 和 Comments.
            ScoreInfoList = ScoreInfoList.OrderBy(u => u.PeriodEnd).ToList();
            foreach (ScoreInfo item in ScoreInfoList)
            {
                TimeSpan timeSpan = model.Parameters.StandDate - item.PeriodEnd;
                if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange4)
                {
                    model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate5 * item.Score);

                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange3)
                {
                    model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate4 * item.Score);
                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange2)
                {
                    model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate3 * item.Score);
                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange1)
                {
                    model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate2 * item.Score);
                }
                else
                {
                    model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate1 * item.Score);
                }
                model.UpdateMembersList.Find(u => u.ID == item.MemberID.ID).Comments += "/" + item.Comment.ToString();
            }

            //更新 Rank
            List<MemberInfo> tempMemberInfoList = provider.GetMemberInfos().ToList();
            foreach (UpdateMembers item in model.UpdateMembersList)
            {
                tempMemberInfoList.Find(u => u.ID == item.ID).Score = Int32.Parse(item.UpdateScore.ToString());
            }
            List<DataHelper.MemberRank> tempRankList = DataHelper.GetMemberRank(tempMemberInfoList);
            foreach (UpdateMembers item in model.UpdateMembersList)
            {
                item.UpdateRank = tempRankList.Find(u => u.MemberID == item.ID).Rank;
            }
            model.UpdateMembersList = model.UpdateMembersList.OrderBy(u => u.UpdateRank).ToList();
            return model.UpdateMembersList;
        }
        public FinalScoreManageIndexModel AddToBonusList(FinalScoreManageIndexModel modelInput)
        {
            FinalScoreBonusInfo bonusInfo = FinalScoreBonusInfo.GetModel(modelInput, provider);
           
            modelInput.FinalScoreBonusList.Add(bonusInfo);
            modelInput.Parameters.ActionSteps = 0;            

            return modelInput;
        }

        public FinalScoreManageIndexModel ReviewScoreList(FinalScoreManageIndexModel modelInput)
        {
            modelInput.AddScoreInfoList = new List<AddScoreInfo>();
            modelInput.AddScoreInfoList = GetAddScoreInfo(modelInput);            
            return modelInput;
        }

        internal List<AddScoreInfo> GetAddScoreInfo(FinalScoreManageIndexModel model)
        {
            AddScoreInfo info;
           
            foreach (FinalScoreBonusInfo item in model.FinalScoreBonusList)
            {
                info = model.AddScoreInfoList.Find(u => u.MemberID == item.MemberID);
                StringBuilder Comments = new StringBuilder();
                if (info == null)
                {
                    info = new AddScoreInfo();
                    info.MemberID = item.MemberID;
                    info.MemberName = item.MembernName;
                    info.Score = item.Score;
                    info.Comments = Comments.AppendFormat("{0}/{1}/{2}", item.ChampionTitle, item.BonusTypeDescription, item.Score.ToString()).ToString();
                    info.PeriodEnd = provider.GetChampionshipInfoByID(item.ChampionID).EndDate;
                    model.AddScoreInfoList.Add(info);
                }
                else
                {
                    info.Score = info.Score + item.Score;
                    info.PeriodEnd = DateTime.Compare(info.PeriodEnd, provider.GetChampionshipInfoByID(item.ChampionID).EndDate) > 0 ?
                        info.PeriodEnd : provider.GetChampionshipInfoByID(item.ChampionID).EndDate;
                    info.Comments = Comments.AppendFormat("{4}; {0}/{1}/{2}", item.ChampionTitle, item.BonusTypeDescription, item.Score.ToString(), info.Comments).ToString();                     
                }
            }
            return model.AddScoreInfoList;
        }
        public FinalScoreManageIndexModel SaveBonusAndScoreEntry(FinalScoreManageIndexModel modelInput)
        {
            if (modelInput.FinalScoreBonusList.Count == 0 || modelInput.AddScoreInfoList.Count == 0)
            {
                modelInput.StateMessage = stateMessage_NoScoreOrBonusNeedToBeSaved;
                modelInput.ErrorState = true;
                return modelInput;
            }

            Boolean flag = RecordScoreEntryToDB(modelInput);
            if (flag)
            {
                modelInput = new FinalScoreManageIndexModel();
                modelInput.StateMessage = stateMessage_SavedAndGoToScoreCalcPageAndAdjust;
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput.StateMessage = stateMessage_SaveNotSuccess;
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