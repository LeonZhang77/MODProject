using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;

namespace Wicresoft.BadmintonSystem.Unity
{
    public class DataHelper
    {
        static IBadmintionDataProvider provider;
            
        public DataHelper()
        {
            provider = new BadmintionDataProvider();
        }
        public static IEnumerable<MatchInfo> GetMatchInfos(ChampionshipInfo championshipInfo)
        {
            return provider.GetMatchInfos().Where(u => u.ChampionID.ID == championshipInfo.ID);
        }

        public static IEnumerable<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost)
        {
            IEnumerable<MatchInfo> returnValue = provider.GetMatchInfos();
            long ID = memberInfo.ID;

            if (WinOrLost)
            { returnValue = returnValue.Where(u => u.WinnerID.ID == ID || u.WinnerID2.ID == ID); }
            else
            { returnValue = returnValue.Where(u => u.LoserID.ID == ID || u.LoserID2.ID == ID); }

            return returnValue;
        }

        public  static IEnumerable<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost, ChampionType championType, Boolean equalOrNot)
        {
            IEnumerable<MatchInfo> returnValue = GetMatchInfos(memberInfo, WinOrLost);
            if (equalOrNot)
            { returnValue = returnValue.Where(u => u.ChampionID.ChampionType.Equals(championType)); }
            else
            { returnValue = returnValue.Where(u => !u.ChampionID.ChampionType.Equals(championType)); }
            return returnValue;
        }

        public static IEnumerable<MatchInfo> GetMatchInfos(MemberInfo memberInfo, Boolean WinOrLost, CompetingType competingType)
        {
            IEnumerable<MatchInfo> returnValue = GetMatchInfos(memberInfo, WinOrLost);
            returnValue = returnValue.Where(u => u.ChampionID.CompetingType.Equals(competingType));
            return returnValue;
        }
    }
}
