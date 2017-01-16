using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wicresoft.BadmintonSystem.Entity;

namespace Wicresoft.BadmintonSystem.Unity
{
    public class DataHelper
    {
        public static List<MatchInfo> GetMatchInfos(ChampionshipInfo championshipInfo, List<MatchInfo> matchInfos)
        {
            return matchInfos.Where(u => u.ChampionID.ID == championshipInfo.ID).ToList();
        }

        public static List<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost, List<MatchInfo> matchInfos)
        {
            List<MatchInfo> returnValue = matchInfos;
            long ID = memberInfo.ID;

            if (WinOrLost)
            { returnValue = returnValue.Where(u => u.WinnerID.ID == ID || (u.WinnerID2 != null && u.WinnerID2.ID==ID)).ToList(); }
            else
            { returnValue = returnValue.Where(u => u.LoserID.ID == ID || (u.LoserID2 != null && u.LoserID2.ID == ID)).ToList(); }

            return returnValue;
        }

        public static List<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost, ChampionType championType, Boolean equalOrNot, List<MatchInfo> matchInfos)
        {
            List<MatchInfo> returnValue = GetMatchInfos(memberInfo, WinOrLost, matchInfos);
            if (equalOrNot)
            { returnValue = returnValue.Where(u => u.ChampionID.ChampionType.Equals(championType)).ToList(); }
            else
            { returnValue = returnValue.Where(u => !u.ChampionID.ChampionType.Equals(championType)).ToList(); }
            return returnValue;
        }

        public static List<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost, CompetingType competingType, List<MatchInfo> matchInfos)
        {
            List<MatchInfo> returnValue = GetMatchInfos(memberInfo, WinOrLost, matchInfos);
            returnValue = returnValue.Where(u => u.ChampionID.CompetingType.Equals(competingType)).ToList();
            return returnValue;
        }
    }
}
