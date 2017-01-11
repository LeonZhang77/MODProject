using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;

namespace Wicresoft.BadmintonSystem.DataAccess.DataProvider
{
    public class BadmintionDataProvider : IBadmintionDataProvider
    {
        private DBSource DataSource;
        public BadmintionDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<BonusInfo> GetBonusInfos()
        {
            return this.DataSource.BonusInfos;
        }

        public IEnumerable<BonusInfo> GetBonusInfos(MemberInfo memberInfo)
        {
            return this.DataSource.BonusInfos.Where(u => u.MemberID.ID == memberInfo.ID); ;
        }

        public IEnumerable<ChampionshipInfo> GetChampionshipInfos()
        {
            return this.DataSource.ChampionshipInfos;
        }

        public ChampionshipInfo GetChampionshipInfoByID(long ID)
        {
            return this.DataSource.ChampionshipInfos.ToList().FirstOrDefault(m => m.ID == ID);
        }

        public IEnumerable<ClubInfo> GetClubInfoInfos()
        {
            return this.DataSource.ClubInfos;
        }

        public ClubInfo GetClubInfoByID(long ID)
        { 
            return this.DataSource.ClubInfos.FirstOrDefault(u=>u.ID==ID);
        }

        public IEnumerable<MatchInfo> GetMatchInfos()
        {
            return this.DataSource.MatchInfos;
        }

        public IEnumerable<MemberInfo> GetMemberInfos()
        {
            return this.DataSource.MemberInfos;
        }

        public MemberInfo GetMemberInfoByID(long ID)
        {
            return this.DataSource.MemberInfos.FirstOrDefault(u=>u.ID == ID);
        }

        public IEnumerable<ScoreInfo> GetScoreInfos()
        {
            return this.DataSource.ScoreInfos;
        }

        public void SaveBonusInfo(BonusInfo bonusInfo)
        {
            this.DataSource.BonusInfos.Add(bonusInfo);
            this.DataSource.SaveChanges();
        }

        public void DeleteBonusInfo(BonusInfo bonusInfo)
        {
            if (bonusInfo != null)
            {
                this.DataSource.BonusInfos.Remove(bonusInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void SaveChampionshipInfo(ChampionshipInfo championshipInfo)
        {
            if (championshipInfo != null)
            {
                this.DataSource.ChampionshipInfos.Add(championshipInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void UpdateChampionshipInfo(ChampionshipInfo championshipInfo)
        {
            ChampionshipInfo championship = this.GetChampionshipInfoByID(championshipInfo.ID);
            championship.Title = championshipInfo.Title;
            championship.CompetingType = championshipInfo.CompetingType;
            championship.ChampionType = championshipInfo.ChampionType;
            championship.StartDate = championshipInfo.StartDate;
            championship.EndDate = championshipInfo.EndDate;

            this.DataSource.SaveChanges();
        }

        public void DeleteChampionshipInfo(ChampionshipInfo championshipInfo)
        {
            if (championshipInfo != null)
            {
                this.DataSource.ChampionshipInfos.Remove(championshipInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void SaveClubInfo(ClubInfo clubInfo)
        {
            if (clubInfo != null)
            {
                this.DataSource.ClubInfos.Add(clubInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void DeleteClubInfo(ClubInfo clubInfo)
        {
            if (clubInfo != null)
            {
                this.DataSource.ClubInfos.Remove(clubInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void SaveMatchInfo(MatchInfo matchInfo)
        {
            if (matchInfo != null)
            {
                this.DataSource.MatchInfos.Add(matchInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void DeleteMatchInfo(MatchInfo matchInfo)
        {
            if (matchInfo != null)
            {
                this.DataSource.MatchInfos.Remove(matchInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void SaveMemberInfo(MemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                this.DataSource.MemberInfos.Add(memberInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void UpdateMemberInfo(MemberInfo memberInfo)
        {
            MemberInfo member = this.GetMemberInfoByID(memberInfo.ID);
            member.Name = memberInfo.Name;
            member.Male = memberInfo.Male;
            member.UpdateDate = memberInfo.UpdateDate;
            
            this.DataSource.SaveChanges();
        }
        public void DeleteMemberInfo(MemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                this.DataSource.MemberInfos.Remove(memberInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void SaveScoreInfo(ScoreInfo scoreInfo)
        {
            if (scoreInfo != null)
            {
                this.DataSource.ScoreInfos.Add(scoreInfo);
                this.DataSource.SaveChanges();
            }
        }

        public void DeleteScoreInfo(ScoreInfo scoreInfo)
        {
            if (scoreInfo != null)
            {
                this.DataSource.ScoreInfos.Remove(scoreInfo);
                this.DataSource.SaveChanges();
            }
        }


        public IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations()
        {
            return this.DataSource.MemberAndClubRelations;
        }

        public IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(ClubInfo clubInfo)
        {
            return this.DataSource.MemberAndClubRelations.Where(u => u.ClubID.ID == clubInfo.ID);
        }

        public IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(MemberInfo memberInfo)
        {
            return this.DataSource.MemberAndClubRelations.Where(u => u.MemberID.ID == memberInfo.ID);
        }

        public void SaveMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation)
        {
            if (memberAndClubRelation != null)
            {
                this.DataSource.MemberAndClubRelations.Add(memberAndClubRelation);
                this.DataSource.SaveChanges();
            }
        }

        public void DeleteMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation)
        {
            if (memberAndClubRelation != null)
            {
                this.DataSource.MemberAndClubRelations.Remove(memberAndClubRelation);
                this.DataSource.SaveChanges();
            }
        }
    }
}
