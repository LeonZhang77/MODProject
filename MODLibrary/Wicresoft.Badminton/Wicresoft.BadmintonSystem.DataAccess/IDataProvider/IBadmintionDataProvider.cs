﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.BadmintonSystem.Entity;

namespace Wicresoft.BadmintonSystem.DataAccess.IDataProvider
{
    public interface IBadmintionDataProvider : IBaseDataProvider
    {
        IEnumerable<BonusInfo> GetBonusInfos();

        void SaveBonusInfo(BonusInfo bonusInfo);

        void DeleteBonusInfo(BonusInfo bonusInfo);

        IEnumerable<ChampionshipInfo> GetChampionshipInfos();

        void SaveChampionshipInfo(ChampionshipInfo championshipInfo);

        void DeleteChampionshipInfo(ChampionshipInfo championshipInfo);

        IEnumerable<ClubInfo> GetClubInfoInfos();

        void SaveClubInfo(ClubInfo clubInfo);

        void DeleteClubInfo(ClubInfo clubInfo);

        IEnumerable<MatchInfo> GetMatchInfos();

        IEnumerable<MatchInfo> GetMatchInfos(long ID, Boolean WinOrLost);

        IEnumerable<MatchInfo> GetMatchInfos(long ID, Boolean WinOrLost, ChampionType championType, Boolean equalOrNot);

        IEnumerable<MatchInfo> GetMatchInfos(long ID, Boolean WinOrLost, CompetingType competingType);
        void SaveMatchInfo(MatchInfo matchInfo);

        void DeleteMatchInfo(MatchInfo matchInfo);
        
        IEnumerable<MemberInfo> GetMemberInfos();

        MemberInfo GetMemberInfo(long ID);
        void SaveMemberInfo(MemberInfo memberInfo);

        void DeleteMemberInfo(MemberInfo memberInfo);

        IEnumerable<ScoreInfo> GetScoreInfos();

        void SaveScoreInfo(ScoreInfo scoreInfo);

        void DeleteScoreInfo(ScoreInfo scoreInfo);

        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations();

        void SaveMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);

        void DeleteMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
    }
}
