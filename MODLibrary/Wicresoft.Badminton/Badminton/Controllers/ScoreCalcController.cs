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
        ScoreCalcIndexModel model;
        public ScoreCalcController()
        {
            provider = new BadmintionDataProvider();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            rankList = DataHelper.GetMemberRank(memberList);
            matchList = provider.GetMatchInfos().ToList();
            matchList = matchList.Where(u => !u.Updated && u.Verified).ToList();
            model = new ScoreCalcIndexModel();
            model.WaitingMatchList = GetWaitingMatchList(matchList);
        }

        public ActionResult Index(long searchselectedID = 0)
        {
                        
            return View(model);
        }        

        public ActionResult CalcToReview(ScoreCalcIndexModel modelInput)
        {
            if (matchList.Count == 0)
            {
                return Content("<script>alert('" + "没有等待计算积分的比赛，请返回录入数据审核心页面检查！" + "');location.href='/ScoreCalc/Index'</script>");
            }
            else
            {
                modelInput.WaitingMatchList = GetWaitingMatchList(matchList);
                model = CalcAndGoToReview(modelInput);
                return View("Index", model);
            }
        }

        public ActionResult AdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            if (modelInput.AddScoreInfoList.Count == 0 && modelInput.WaitingMatchList.Count != 0)
            {
                return Content("<script>alert('" + "还有等待计算积分的比赛，请先计算积分!" + "');location.href='/ScoreCalc/Index'</script>");
            }
            else
            {
                List<ScoreInfo> scoreInfoList = provider.GetScoreInfos().ToList();
                model = modelInput;
                ScoreInfo tempInfo;
                foreach (AddScoreInfo item in model.AddScoreInfoList)
                {
                    tempInfo = AddScoreInfo.GetEntity(item);
                    scoreInfoList.Add(tempInfo);
                }
                model.UpdateMemberList = GetUpdateMemberList(scoreInfoList);
                return View("Index", "Model");
            }
        }
       
        public ActionResult SaveBonusAndScoreEntry(ScoreCalcIndexModel modelInput)
        {
            model = modelInput;
            Boolean flag = RecordScoreEntryToDB();            
            if (flag)
            {
                return Content("<script>alert('" + "保存成功, 请记得去按周期调整积分！" + "');location.href='/ScoreCalc/Index'</script>");
            }
            else
            {
                return Content("<script>alert('" + "保存没有成功！" + "');location.href='/ScoreCalc/Index'</script>");
            }
        }

        public ActionResult OnlyAdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            if(modelInput.WaitingMatchList.Count != 0)
            {
                return Content("<script>alert('" + "还有等待计算积分的比赛，请先计算积分并保存！" + "');location.href='/ScoreCalc/Index'</script>");
            }
            else
            {
                model = modelInput;
                model.UpdateMemberList = GetUpdateMemberList(provider.GetScoreInfos().ToList());
                return View("Index", "Model");
            }
        }

        internal List<ScoreUpdateMember> GetUpdateMemberList(List<ScoreInfo> ScoreInfoList)
        {
            ScoreUpdateMember updateMember;
            List<MemberInfo>  memberInfoList = provider.GetMemberInfos().ToList();
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
            ScoreInfoList = ScoreInfoList.OrderBy(u => u.CalculateDate).ToList();
            foreach (ScoreInfo item in ScoreInfoList)
            {
                TimeSpan timeSpan = DateTime.Now - item.CalculateDate;
                if ((int)timeSpan.TotalDays > 7*model.Parameters.DateRange4)
                {
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += long.Parse(Math.Round(model.Parameters.Rate5 * item.Score).ToString());

                }
                else if((int)timeSpan.TotalDays > 7*model.Parameters.DateRange3)
                { 
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += long.Parse(Math.Round(model.Parameters.Rate4 * item.Score).ToString());
                }
                else if((int)timeSpan.TotalDays > 7*model.Parameters.DateRange2)
                { 
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += long.Parse(Math.Round(model.Parameters.Rate3 * item.Score).ToString());
                }
                else if ((int)timeSpan.TotalDays > 7 * model.Parameters.DateRange1)
                { 
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += long.Parse(Math.Round(model.Parameters.Rate2 * item.Score).ToString());
                }
                else 
                { 
                    model.UpdateMemberList.Find(u => u.ID == item.MemberID.ID).UpdateScore += long.Parse(Math.Round(model.Parameters.Rate1 * item.Score).ToString());
                }
                model.UpdateMemberList.Find(u=>u.ID == item.MemberID.ID).Comments += "/" + item.Comment.ToString();
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
                        
            return model.UpdateMemberList;
        }

        public ActionResult SaveScoreAfterAdjustAccordingToDateRange(ScoreCalcIndexModel modelInput)
        {
            Boolean flag = UpdateMemberScore(modelInput.UpdateMemberList);
            if (flag)
            {
                return Content("<script>alert('" + "还有等待计算积分的比赛，请先计算积分并保存！" + "');location.href='/ScoreCalc/Index'</script>");
            }
            else
            {
                return View("Index", "Model");
            }
        }

        public Boolean RecordScoreEntryToDB()
        {

            try
            {
                SaveBonusInfors(model.AddBonusInfoList);
                SaveScoreinfors(model.AddScoreInfoList);
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
                    provider.SaveMatchInfo(matchInfo);
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
                
                if(waitingMatch.Winner2Name!=null)
                {
                    if(Math.Abs(provider.GetMemberInfoByID(waitingMatch.Winner1ID).Score +
                        provider.GetMemberInfoByID(waitingMatch.Winner2ID).Score -
                        provider.GetMemberInfoByID(waitingMatch.Loser1ID).Score -
                        provider.GetMemberInfoByID(waitingMatch.Loser2ID).Score) > model.Parameters.DoubleIngore)
                    {
                        flag=true;
                    }
                }
                else
                {
                    if(Math.Abs(provider.GetMemberInfoByID(waitingMatch.Winner1ID).Score - 
                        provider.GetMemberInfoByID(waitingMatch.Loser1ID).Score) > model.Parameters.SingleIngore)
                    {
                        flag=true;
                    }
                }

                if (flag)
                {
                    waitingMatch.Comments = "原积分差距太大，忽略";
                }
                else
                {
                    model = CalcScore(model, waitingMatch);
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
                    info.Comments = item.ChampionTitle + "/" + item.BonusTypeDescription +"/" + item.Score.ToString();
                    model.AddScoreInfoList.Add(info);
                }
                else
                {
                    info.Score = info.Score + item.Score;
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
            //决赛区积分
            if(matchInfo.MatchType!=null)
            {
                model.AddBonusInfoList = CalcFinalScore(model, matchInfo);
            }

            return model;
        }

        internal List<AddBonusInfo> CalcFinalScore(ScoreCalcIndexModel model, MatchInfo matchInfo)
        {
            Boolean isSingles = false;
            AddBonusInfo info;
            long WinScore = model.Parameters.Top8Win;
            long LoseScore = model.Parameters.Top8Lose;

            if (EnumHelper.GetEnumDescription(matchInfo.ChampionID.CompetingType).Contains("Single"))
            {
                isSingles = true;
            }
            String matchType = EnumHelper.GetEnumDescription(matchInfo.MatchType);
            if (matchType.Equals("Final"))
            {
                WinScore = model.Parameters.FinalWin;
                LoseScore = model.Parameters.FinalLose;
            }
            else if (matchType.Equals("Top4"))
            {
                WinScore = model.Parameters.Top8Win;
                LoseScore = model.Parameters.Top8Lose;
            }

            info = InitinalAddBonusInfo(matchInfo);
            info.MemberID = matchInfo.WinnerID.ID;
            info.MembernName = matchInfo.WinnerID.Name;
            info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
            info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
            info.Score = WinScore;
            model.AddBonusInfoList.Add(info);

            info = InitinalAddBonusInfo(matchInfo);
            info.MemberID = matchInfo.LoserID.ID;
            info.MembernName = matchInfo.LoserID.Name;
            info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
            info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
            info.Score = LoseScore;
            model.AddBonusInfoList.Add(info);

            if (!isSingles)
            {
                info = InitinalAddBonusInfo(matchInfo);
                info.MemberID = matchInfo.WinnerID2.ID;
                info.MembernName = matchInfo.WinnerID2.Name;
                info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
                info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
                info.Score = WinScore;
                model.AddBonusInfoList.Add(info);

                info = InitinalAddBonusInfo(matchInfo);
                info.MemberID = matchInfo.LoserID2.ID;
                info.MembernName = matchInfo.LoserID2.Name;
                info.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
                info.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
                info.Score = LoseScore;
                model.AddBonusInfoList.Add(info);
            }
            return model.AddBonusInfoList;
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
                    rankList.Find(u => u.MemberID == matchInfo.WinnerID2.ID).Rank)/2);
                LosePartRank = Math.Abs((rankList.Find(u => u.MemberID == matchInfo.LoserID.ID).Rank + 
                    rankList.Find(u => u.MemberID == matchInfo.LoserID2.ID).Rank)/2);
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
            info.ChampionTitle = matchInfo.MatchDate.ToShortDateString()+ "-" + matchInfo.ChampionID.Title + "/";
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