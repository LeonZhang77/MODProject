using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Wicresoft.BadmintonSystem.WebService.Models;

namespace Wicresoft.BadmintonSystem.WebService.Controllers
{
    public class ScoreListController : BaseController
    {
        public IBadmintionDataProvider provider;

        public ScoreListController()
        { 
            this.provider = new BadmintionDataProvider();
        }
        public JsonResult Index()
        {
            List<ScoreMemberModel> modelList = GetScoreList();

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        internal List<ScoreMemberModel> GetScoreList()
        {
            List<ScoreMemberModel> returnList = new List<ScoreMemberModel>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            List<DataHelper.MemberRank> rankList = DataHelper.GetMemberRank(memberList);
            ScoreMemberModel temp;
            foreach (var item in memberList)
            {
                temp = new ScoreMemberModel();
                temp.ID = item.ID;
                temp.MemberName = item.Name;
                temp.Male = item.Male;
                temp.Score = item.Score;
                temp.WinRate = GetWinRate(item) * 100;
                temp.AverageWinRate = GetAverageWinRate(item) * 100;
                //temp.Ranking = 1;         
                temp.Ranking = rankList.Find(u => u.MemberID == item.ID).Rank;
                returnList.Add(temp);
            }
            var list = from r in returnList orderby r.Ranking select r;
            returnList = list.ToList();
            return returnList;
        }

        internal double GetWinRate(MemberInfo memberInfo)
        {
            double returnValue = 0;
            List<MatchInfo> matchInfos = provider.GetMatchInfos().ToList();
            List<MatchInfo> matchList = DataHelper.GetMatchInfos(memberInfo, true, ChampionType.Normal, false, matchInfos).ToList();
            int WinCount = matchList.Count();
            matchList = DataHelper.GetMatchInfos(memberInfo, false, ChampionType.Normal, false, matchInfos).ToList();
            int LostCount = matchList.Count();
            if ((WinCount + LostCount) != 0)
            {
                returnValue = (double)WinCount / ((double)WinCount + (double)LostCount);
            }

            returnValue = System.Math.Round(returnValue, 2);
            return returnValue;
        }

        internal double GetAverageWinRate(MemberInfo memberInfo)
        {
            double returnValue = 0;
            List<MatchInfo> matchInfos = provider.GetMatchInfos().ToList();
            List<MatchInfo> matchList = DataHelper.GetMatchInfos(memberInfo, true, matchInfos).ToList();
            int WinCount = matchList.Count();
            matchList = DataHelper.GetMatchInfos(memberInfo, false, matchInfos).ToList();
            int LostCount = matchList.Count();
            if ((WinCount + LostCount) != 0)
            {
                returnValue = (double)WinCount / ((double)WinCount + (double)LostCount);
            }

            returnValue = System.Math.Round(returnValue, 2);
            return returnValue;
        }
    }
}