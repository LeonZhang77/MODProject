using System;
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
        IEnumerable<ChampionshipInfo> GetActiveChampionshipInfos();
        ChampionshipInfo GetChampionshipInfoByID(long ID);
        ChampionshipInfo GetChampionshipInfoByName(string Title);
        void SaveChampionshipInfo(ChampionshipInfo championshipInfo);
        void UpdateChampionshipInfo(ChampionshipInfo championshipInfo);
        void DeleteChampionshipInfo(ChampionshipInfo championshipInfo);
        
        IEnumerable<ClubInfo> GetClubInfoInfos();
        ClubInfo GetClubInfoByID(long ID);
        ClubInfo GetClubInfoByName(string Name);
        void SaveClubInfo(ClubInfo clubInfo);
        void UpdateClubInfo(ClubInfo clubInfo);
        void DeleteClubInfo(ClubInfo clubInfo);
        
        IEnumerable<MatchInfo> GetMatchInfos();
        MatchInfo GetMatchInfoByID(long ID);
        IEnumerable<MatchInfo> GetMatchInfoByChampionID(long ID);
        void SaveMatchInfo(MatchInfo matchInfo);
        void UpdateMatchInfo(MatchInfo matchInfo);
        void BatchUpdateMatchInfoValid(List<MatchInfo> matchList);
        void DeleteMatchInfo(MatchInfo matchInfo);        
        
        IEnumerable<MemberInfo> GetMemberInfos();
        MemberInfo GetMemberInfoByID(long ID);
        MemberInfo GetMemberInfoByName(string Name);
        void SaveMemberInfo(MemberInfo memberInfo);
        void UpdateMemberInfo(MemberInfo memberInfo);
        void DeleteMemberInfo(MemberInfo memberInfo);
        
        IEnumerable<ScoreInfo> GetScoreInfos();
        void SaveScoreInfo(ScoreInfo scoreInfo);
        void DeleteScoreInfo(ScoreInfo scoreInfo);
        
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations();
        MemberAndClubRelation GetMemberAndClubRelationByID(long ID);
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(ClubInfo clubInfo);
        IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(MemberInfo memberInfo);
        void SaveMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
        void UpdateMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
        void DeleteMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation);
        void BatchDeleteMemberAndClubRelation(List<MemberAndClubRelation> memberAndClubRelation);
        void BatchAddMemberAndClubRelation(List<MemberAndClubRelation> memberAndClubRelation);
    }
}
