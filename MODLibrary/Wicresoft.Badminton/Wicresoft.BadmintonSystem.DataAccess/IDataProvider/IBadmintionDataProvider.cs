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
        IEnumerable<BonusInfo> GetBonusInfos(MemberInfo memberInfo);
        void SaveBonusInfo(BonusInfo bonusInfo);   
        void DeleteBonusInfo(BonusInfo bonusInfo);
        
        IEnumerable<ChampionshipInfo> GetChampionshipInfos();
        ChampionshipInfo GetChampionshipInfoByID(long ID);
        void SaveChampionshipInfo(ChampionshipInfo championshipInfo);
        void UpdateChampionshipInfo(ChampionshipInfo championshipInfo);
        void DeleteChampionshipInfo(ChampionshipInfo championshipInfo);
        
        IEnumerable<ClubInfo> GetClubInfoInfos();
        ClubInfo GetClubInfoByID(long ID);
        void SaveClubInfo(ClubInfo clubInfo);
        void DeleteClubInfo(ClubInfo clubInfo);
        
        IEnumerable<MatchInfo> GetMatchInfos();
        void SaveMatchInfo(MatchInfo matchInfo);
        void DeleteMatchInfo(MatchInfo matchInfo);        
        
        IEnumerable<MemberInfo> GetMemberInfos();
        MemberInfo GetMemberInfoByID(long ID);
        void SaveMemberInfo(MemberInfo memberInfo);
        void UpdateMemberInfo(MemberInfo memberInfo);
        void DeleteMemberInfo(MemberInfo memberInfo);
        
        IEnumerable<ScoreInfo> GetScoreInfos();
        void SaveScoreInfo(ScoreInfo scoreInfo);
        void DeleteScoreInfo(ScoreInfo scoreInfo);
        
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations();
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(ClubInfo clubInfo);
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(MemberInfo memberInfo);
        void SaveMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
        void DeleteMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
    }
}
