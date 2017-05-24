using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Badminton.Models.ScoreCalc;


namespace Badminton.Controllers
{
    public class ScoreCalcController : Controller
    {
        IBadmintionDataProvider provider;
        List<DataHelper.MemberRank> rankList;
        List<MatchInfo> matchList;
        public ScoreCalcController()
        {
            provider = new BadmintionDataProvider();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            rankList = DataHelper.GetMemberRank(memberList);
            matchList = provider.GetMatchInfos().ToList();
            matchList = matchList.Where(u => !u.Updated && u.Verified).ToList();
        }

        public ActionResult Index(long searchselectedID = 0)
        {
            ScoreCalcIndexModel model = new ScoreCalcIndexModel();
            model.WaitingMatchList = GetWaitingMatchList(matchList);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ScoreCalcIndexModel modelInput)
        {
            ActionResult returnAction = new ViewResult();
            modelInput.ErrorState = false;

            switch (modelInput.Parameters.ActionSteps)
            {
                case 1:
                    modelInput = CalcToReview(modelInput);
                    break;
                case 2:
                    modelInput = AdjustAccordingToDateRange(modelInput);
                    break;
                case 3:
                    modelInput = SaveBonusAndScoreEntry(modelInput);
                    break;
                case 4:
                    modelInput = OnlyAdjustAccordingToDateRange(modelInput);
                    break;
                case 5:
                    modelInput = SaveScoreAfterAdjustAccordingToDateRange(modelInput);
                    break;
                default:
                    modelInput = new ScoreCalcIndexModel();
                    break;

            }

            return View("Index", modelInput);
        }

        public ScoreCalcIndexModel CalcToReview(ScoreCalcIndexModel modelInput)
        {

            if (modelInput.WaitingMatchList.Count == 0)
            {
                modelInput.StateMessage = "没有等待计算积分的比赛，请返回录入数据审核页面检查！";
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput = CalcAndGoToReview(modelInput);
            }
            return modelInput;
        }

        public ScoreCalcIndexModel AdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            if (modelInput.AddScoreInfoList.Count == 0 && modelInput.WaitingMatchList.Count != 0)
            {
                modelInput.StateMessage = "还有等待计算积分的比赛，请先计算积分!";
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
                modelInput.UpdateMemberList = GetUpdateMemberList(scoreInfoList, modelInput);

            }
            return modelInput;
        }

        public ScoreCalcIndexModel SaveBonusAndScoreEntry(ScoreCalcIndexModel modelInput)
        {
            if (modelInput.AddBonusInfoList.Count == 0 && modelInput.AddScoreInfoList.Count == 0)
            {
                modelInput.StateMessage = "没有需要保存的Score和Bonus, 请先计算并检查！";
                modelInput.ErrorState = true;
                return modelInput;
            }

            Boolean flag = RecordScoreEntryToDB(modelInput);
            if (flag)
            {
                modelInput = new ScoreCalcIndexModel();
                modelInput.StateMessage = "保存成功, 请记得去按周期调整积分！";
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput.StateMessage = "保存没有成功！";
                modelInput.ErrorState = true;
            }

            return modelInput;
        }

        public ScoreCalcIndexModel OnlyAdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            if (modelInput.WaitingMatchList.Count != 0)
            {
                modelInput.StateMessage = "还有等待计算积分的比赛，请先计算积分并保存！";
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput.UpdateMemberList = GetUpdateMemberList(provider.GetScoreInfos().ToList(), modelInput);

            }
            return modelInput;
        }

        internal List<ScoreUpdateMember> GetUpdateMemberList(List<ScoreInfo> ScoreInfoList, ScoreCalcIndexModel modelInput)
        {
            ScoreUpdateMember updateMember;
            List<MemberInfo> memberInfoList = provider.GetMemberInfos().ToList();
            ScoreCalcIndexModel model = modelInput;
            //初始化 model.UpdateMemberList.
            foreach (MemberInfo item in memberInfoList)
            {
                updateMember = new ScoreUpdateMember();
                updateMember.ID = item.ID;
                updateMember.MemberName = item.Name;
                updateMember.OriginalScore = item.Score;
                updateMember.OriginalRank = rankList.Find(u => u.MemberID == item.ID).Rank;
                updateMember.UpdateScore = 100;
                model.UpdateMemberList.Add(updateMember);
            }

            // 更新 update Score 和 Comments.
            ScoreInfoList = ScoreInfoList.OrderBy(u => u.PeriodEnd).ToList();
            foreach (ScoreInfo item in ScoreInfoList)
            {
                TimeSpan timeSpan = model.Parameters.StandDate - item.PeriodEnd;
                if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange4)
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate5 * item.Score);

                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange3)
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate4 * item.Score);
                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange2)
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate3 * item.Score);
                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange1)
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate2 * item.Score);
                }
                else
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += (long)Math.Round(model.Parameters.Rate1 * item.Score);
                }
                model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).Comments += "/" + item.Comment.ToString();
            }

            //更新 Rank
            List<MemberInfo> tempMemberInfoList = provider.GetMemberInfos().ToList();
            foreach (ScoreUpdateMember item in model.UpdateMemberList)
            {
                tempMemberInfoList.Find(u => u.ID == item.ID).Score = Int32.Parse(item.UpdateScore.ToString());
            }
            List<DataHelper.MemberRank> tempRankList = DataHelper.GetMemberRank(tempMemberInfoList);
            foreach (ScoreUpdateMember item in model.UpdateMemberList)
            {
                item.UpdateRank = tempRankList.Find(u => u.MemberID == item.ID).Rank;
            }
            model.UpdateMemberList = model.UpdateMemberList.OrderBy(u => u.UpdateRank).ToList();
            return model.UpdateMemberList;
        }

        public ScoreCalcIndexModel SaveScoreAfterAdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            if (modelInput.UpdateMemberList.Count == 0)
            {
                modelInput.StateMessage = "请先进行周期调整计算并预览！";
                modelInput.ErrorState = true;
                return modelInput;
            }

            if (modelInput.WaitingMatchList.Count != 0)
            {
                modelInput.StateMessage = "还有等待计算积分的比赛，请先计算积分并保存！";
                modelInput.ErrorState = true;
                return modelInput;
            }

            Boolean flag = UpdateMemberScore(modelInput.UpdateMemberList);
            if (flag)
            {
                modelInput = new ScoreCalcIndexModel();
                modelInput.StateMessage = "保存成功！";
                modelInput.ErrorState = true;
            }
            else
            {
                modelInput.StateMessage = "保存没有成功！";
                modelInput.ErrorState = true;
            }

            return modelInput;
        }

        public Boolean RecordScoreEntryToDB(ScoreCalcIndexModel modelInput)
        {

            try
            {
                SaveBonusInfors(modelInput.AddBonusInfoList);
                SaveScoreinfors(modelInput.AddScoreInfoList);
            }
            catch (Exception ex) { return false; };

            return true;
        }

        internal void SaveBonusInfors(List<AddBonusInfo> AddBonusInfoList)
        {
            foreach (AddBonusInfo item in AddBonusInfoList)
            {
                BonusInfo info = AddBonusInfo.GetEntity(item);
                provider.SaveBonusInfo(info);
                MatchInfo matchInfo = provider.GetMatchInfoByID(info.MatchID);
                if (matchInfo.Updated == null || matchInfo.Updated == false)
                {
                    matchInfo.Updated = true;
                    provider.UpdateMatchInfo(matchInfo);
                }
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

        internal Boolean UpdateMemberScore(List<ScoreUpdateMember> UpdateMemberList)
        {
            try
            {
                foreach (ScoreUpdateMember item in UpdateMemberList)
                {
                    MemberInfo info = ScoreUpdateMember.GetEntity(item);
                    provider.UpdateMemberInfo(info);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public ScoreCalcIndexModel CalcAndGoToReview(ScoreCalcIndexModel model)
        {
            Boolean flag = false;
            model.AddBonusInfoList = new List<AddBonusInfo>();
            foreach (WaitingMatchModel waitingMatch in model.WaitingMatchList)
            {

                if (waitingMatch.Winner2Name != null)
                {
                    if (Math.Abs((provider.GetMemberInfoByID(waitingMatch.Winner1ID).Score +
                        provider.GetMemberInfoByID(waitingMatch.Winner2ID).Score) / 2 -
                        (provider.GetMemberInfoByID(waitingMatch.Loser1ID).Score +
                        provider.GetMemberInfoByID(waitingMatch.Loser2ID).Score) / 2) > model.Parameters.DoubleIgnore)
                    {
                        flag = true;
                    }
                }
                else
                {
                    if (Math.Abs(provider.GetMemberInfoByID(waitingMatch.Winner1ID).Score -
                        provider.GetMemberInfoByID(waitingMatch.Loser1ID).Score) > model.Parameters.SingleIgnore)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    waitingMatch.Comments = "原积分差距太大，忽略";
                }
                else
                {
                    model = CalcScore(model, waitingMatch);//计算基础积分和排名积分
                }
            }

            model.AddScoreInfoList = new List<AddScoreInfo>();
            model.AddScoreInfoList = GetAddScoreInfo(model);

            return model;
        }

        internal List<AddScoreInfo> GetAddScoreInfo(ScoreCalcIndexModel model)
        {
            AddScoreInfo info;
            foreach (AddBonusInfo item in model.AddBonusInfoList)
            {
                info = model.AddScoreInfoList.Find(u => u.MemberID == item.MemberID);
                if (info == null)
                {
                    info = new AddScoreInfo();
                    info.MemberID = item.MemberID;
                    info.MemberName = item.MembernName;
                    info.Score = item.Score;
                    info.Comments = item.ChampionTitle + "/" + item.BonusTypeDescription + "/" + item.Score.ToString();
                    info.PeriodEnd = provider.GetMatchInfoByID(item.MatchID).MatchDate;
                    model.AddScoreInfoList.Add(info);
                }
                else
                {
                    info.Score = info.Score + item.Score;
                    info.PeriodEnd = DateTime.Compare(info.PeriodEnd, provider.GetMatchInfoByID(item.MatchID).MatchDate) > 0 ?
                     info.PeriodEnd : provider.GetMatchInfoByID(item.MatchID).MatchDate;
                    info.Comments = info.Comments + "; " + item.ChampionTitle + "/" + item.BonusTypeDescription + "/" + item.Score.ToString();
                }
            }
            return model.AddScoreInfoList;
        }

        internal ScoreCalcIndexModel CalcScore(ScoreCalcIndexModel model, WaitingMatchModel waitingMatch)
        {
            MatchInfo matchInfo = provider.GetMatchInfoByID(waitingMatch.ID);
            //基础积分
            model.AddBonusInfoList = CalcBaseScore(model, matchInfo);
            //排名积分
            model.AddBonusInfoList = CalcRankScore(model, matchInfo);

            return model;
        }

        internal List<AddBonusInfo> CalcRankScore(ScoreCalcIndexModel model, MatchInfo matchInfo)
        {
            long WinPartRank, LosePartRank;
            AddBonusInfo info;
            long bonusScore = 0;
            Boolean isSingles = false;
            if (EnumHelper.GetEnumDescription(matchInfo.ChampionID.CompetingType).Contains("Single"))
            {
                isSingles = true;
            }
            if (isSingles)
            {
                WinPartRank = rankList.Find(u => u.MemberID == matchInfo.WinnerID.ID).Rank;
                LosePartRank = rankList.Find(u => u.MemberID == matchInfo.LoserID.ID).Rank;
            }
            else
            {
                WinPartRank = Math.Abs((rankList.Find(u => u.MemberID == matchInfo.WinnerID.ID).Rank +
                    rankList.Find(u => u.MemberID == matchInfo.WinnerID2.ID).Rank) / 2);
                LosePartRank = Math.Abs((rankList.Find(u => u.MemberID == matchInfo.LoserID.ID).Rank +
                    rankList.Find(u => u.MemberID == matchInfo.LoserID2.ID).Rank) / 2);
            }

            if (LosePartRank < WinPartRank)
            {
                if (LosePartRank <= model.Parameters.Rank1End)
                {
                    bonusScore = model.Parameters.RankBonus1;
                    if (WinPartRank <= model.Parameters.Rank1End) { bonusScore = bonusScore / 2; }
                }
                else if (LosePartRank <= model.Parameters.Rank2End)
                {
                    bonusScore = model.Parameters.RankBonus2;
                    if (WinPartRank <= model.Parameters.Rank2End) { bonusScore = bonusScore / 2; }
                }
                else if (LosePartRank <= model.Parameters.Rank3End)
                {
                    bonusScore = model.Parameters.RankBonus3;
                    if (WinPartRank <= model.Parameters.Rank3End) { bonusScore = bonusScore / 2; }
                }
                else if (LosePartRank <= model.Parameters.Rank4End)
                {
                    bonusScore = model.Parameters.RankBonus4;
                    if (WinPartRank <= model.Parameters.Rank4End) { bonusScore = bonusScore / 2; }
                }
            }

            if (bonusScore > 0)
            {
                info = InitinalAddBonusInfo(matchInfo);
                info.MemberID = matchInfo.WinnerID.ID;
                info.MembernName = matchInfo.WinnerID.Name;
                info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Rank).ToString());
                info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Rank);
                info.Score = bonusScore;
                model.AddBonusInfoList.Add(info);
                if (!isSingles)
                {
                    info = InitinalAddBonusInfo(matchInfo);
                    info.MemberID = matchInfo.WinnerID2.ID;
                    info.MembernName = matchInfo.WinnerID2.Name;
                    info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Rank).ToString());
                    info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Rank);
                    info.Score = bonusScore;
                    model.AddBonusInfoList.Add(info);
                }
            }

            return model.AddBonusInfoList;
        }

        internal List<AddBonusInfo> CalcBaseScore(ScoreCalcIndexModel model, MatchInfo matchInfo)
        {
            AddBonusInfo info;
            Boolean isSingles = false;
            if (matchInfo.ChampionID.CompetingType.Equals(CompetingType.MaleSin) ||
                    matchInfo.ChampionID.CompetingType.Equals(CompetingType.FemaleSin) ||
                    matchInfo.ChampionID.CompetingType.Equals(CompetingType.MixSin))
            {
                isSingles = true;
            }

            info = InitinalAddBonusInfo(matchInfo);
            info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Base).ToString());
            info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Base);
            info.MemberID = matchInfo.WinnerID.ID;

            info.MembernName = matchInfo.WinnerID.Name;
            if (isSingles)
            { info.Score = model.Parameters.SingleWin; }
            else
            { info.Score = model.Parameters.DoubleWin; }
            model.AddBonusInfoList.Add(info);

            info = InitinalAddBonusInfo(matchInfo);
            info.MemberID = matchInfo.LoserID.ID;
            info.MembernName = matchInfo.LoserID.Name;
            info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Base).ToString());
            info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Base);
            if (isSingles)
            { info.Score = model.Parameters.SingleLose; }
            else
            { info.Score = model.Parameters.DoubleLose; }
            model.AddBonusInfoList.Add(info);

            if (!isSingles)
            {
                info = InitinalAddBonusInfo(matchInfo);
                info.MemberID = matchInfo.WinnerID2.ID;
                info.MembernName = matchInfo.WinnerID2.Name;
                info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Base).ToString());
                info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Base);
                info.Score = model.Parameters.DoubleWin;
                model.AddBonusInfoList.Add(info);

                info = InitinalAddBonusInfo(matchInfo);
                info.MemberID = matchInfo.LoserID2.ID;
                info.MembernName = matchInfo.LoserID2.Name;
                info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Base).ToString());
                info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Base);
                info.Score = model.Parameters.DoubleLose;
                model.AddBonusInfoList.Add(info);
            }

            return model.AddBonusInfoList;
        }

        internal AddBonusInfo InitinalAddBonusInfo(MatchInfo matchInfo)
        {
            AddBonusInfo info = new AddBonusInfo();
            info.ChampionID = matchInfo.ChampionID.ID;
            info.ChampionTitle = matchInfo.MatchDate.ToShortDateString() + "-" + matchInfo.ChampionID.Title + "/";
            if (EnumHelper.GetEnumDescription(matchInfo.ChampionID.CompetingType).Contains("Single"))
            {
                info.ChampionTitle = info.ChampionTitle + matchInfo.WinnerID.Name + ">" + matchInfo.LoserID.Name;
            }
            else
            {
                info.ChampionTitle = info.ChampionTitle + matchInfo.WinnerID.Name + "/" + matchInfo.WinnerID2.Name + ">" + matchInfo.LoserID.Name + "/" + matchInfo.LoserID2.Name;
            }
            info.MatchID = matchInfo.ID;
            info.CreateTime = DateTime.Now;
            return info;
        }

        internal List<WaitingMatchModel> GetWaitingMatchList(List<MatchInfo> matchList)
        {
            List<WaitingMatchModel> returnList = new List<WaitingMatchModel>();
            WaitingMatchModel model;
            foreach (MatchInfo item in matchList)
            {
                model = WaitingMatchModel.GetViewModel(item);
                returnList.Add(model);
            }
            return returnList;
        }
    }
}