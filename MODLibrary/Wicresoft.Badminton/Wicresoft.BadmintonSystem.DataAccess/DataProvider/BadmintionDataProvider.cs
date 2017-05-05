﻿using System;
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
        public IEnumerable<ChampionshipInfo> GetActiveChampionshipInfos()
        {
            return this.DataSource.ChampionshipInfos.Where(u=>u.IsActive==true);
        }

        public ChampionshipInfo GetChampionshipInfoByID(long ID)
        {
            return this.DataSource.ChampionshipInfos.ToList().FirstOrDefault(m => m.ID == ID);
        }
        public ChampionshipInfo GetChampionshipInfoByName(string Title)
        {
            return this.DataSource.ChampionshipInfos.ToList().FirstOrDefault(m => m.Title == Title);
        }
        
        public IEnumerable<ClubInfo> GetClubInfoInfos()
        {
            return this.DataSource.ClubInfos;
        }

        public ClubInfo GetClubInfoByID(long ID)
        { 
            return this.DataSource.ClubInfos.FirstOrDefault(u=>u.ID == ID);
        }
        public ClubInfo GetClubInfoByName(string Name)
        {
            return this.DataSource.ClubInfos.FirstOrDefault(u => u.Name == Name);
        }
        public IEnumerable<MatchInfo> GetMatchInfos()
        {
            return this.DataSource.MatchInfos;
        }

        public MatchInfo GetMatchInfoByID(long ID)
        {
            return this.DataSource.MatchInfos.FirstOrDefault(u => u.ID == ID);
        }

        public IEnumerable<MemberInfo> GetMemberInfos()
        {
            return this.DataSource.MemberInfos;
        }

        public MemberInfo GetMemberInfoByID(long ID)
        {
            return this.DataSource.MemberInfos.FirstOrDefault(u=>u.ID == ID);
        }
        public MemberInfo GetMemberInfoByName(string Name)
        {
            return this.DataSource.MemberInfos.FirstOrDefault(u => u.Name == Name);
        }
        public void UpdateClubInfo(ClubInfo clubInfo)
        {
            ClubInfo info = this.GetClubInfoByID(clubInfo.ID);
            info.Name = clubInfo.Name;
            info.Description = clubInfo.Description;

            this.DataSource.SaveChanges();
        }
        public IEnumerable<ScoreInfo> GetScoreInfos()
        {
            return this.DataSource.ScoreInfos;
        }

        public void SaveBonusInfo(BonusInfo bonusInfo)
        {
            if (bonusInfo != null) 
            {
                bonusInfo.ChampionID = this.GetChampionshipInfoByID(bonusInfo.ChampionID.ID);
                bonusInfo.MemberID = this.GetMemberInfoByID(bonusInfo.MemberID.ID);
                this.DataSource.BonusInfos.Add(bonusInfo);
                this.DataSource.SaveChanges();
            }
            
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
            championship.IsActive = championship.IsActive;

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
        public IEnumerable<MatchInfo> GetMatchInfoByChampionID(long ID) 
        {
            return this.DataSource.MatchInfos.Where(u => u.ChampionID.ID == ID);
        }
        public void UpdateMatchInfo(MatchInfo matchInfo)
        {
            MatchInfo match = this.GetMatchInfoByID(matchInfo.ID);
            
            match.Compensation = matchInfo.Compensation;
            match.CreateTime = matchInfo.CreateTime;
            match.Ignore = matchInfo.Ignore;
            match.WinnerPoints = matchInfo.WinnerPoints;
            match.LoserPoints = matchInfo.LoserPoints;
            match.MatchDate = matchInfo.MatchDate;
            match.Updated = matchInfo.Updated;
            match.VerifyDate = matchInfo.VerifyDate;
            match.Verified = matchInfo.Verified;
            match.MatchType = matchInfo.MatchType;
            match.ChampionID = matchInfo.ChampionID;
            match.InputPersonID = matchInfo.InputPersonID;
            match.WinnerID = matchInfo.WinnerID;
            match.LoserID = matchInfo.LoserID;

            if (matchInfo.WinnerID2 != null && matchInfo.LoserID2 != null)
            {
                match.WinnerID2 = matchInfo.WinnerID2;
                match.LoserID2 = matchInfo.LoserID2;
            }

            this.DataSource.SaveChanges();
        }

        public void BatchUpdateMatchInfoValid(List<MatchInfo> matchList) 
        {
            foreach (MatchInfo item in matchList)
            {
                MatchInfo match = this.GetMatchInfoByID(item.ID);
                match.Verified = item.Verified;
                match.VerifyDate = item.VerifyDate;
            }
            this.DataSource.SaveChanges();
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
            member.Score = memberInfo.Score;
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
                scoreInfo.MemberID = this.GetMemberInfoByID(scoreInfo.MemberID.ID);
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
        public MemberAndClubRelation GetMemberAndClubRelationByID(long ID)
        {
            return this.DataSource.MemberAndClubRelations.FirstOrDefault(u => u.ID == ID);
        }
        public IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(ClubInfo clubInfo)
        {
            return this.DataSource.MemberAndClubRelations.Where(u => u.ClubID.ID == clubInfo.ID);
        }

        public IEnumerable<MemberAndClubRelation> GetMemberAndClubRelations(MemberInfo memberInfo)
        {
            return this.DataSource.MemberAndClubRelations.Where(u => u.MemberID.ID == memberInfo.ID);
        }
        public void UpdateMemberAndClubRelation(MemberAndClubRelation memberAndClubRelation)
        {
            if (memberAndClubRelation != null)
            {
                MemberAndClubRelation info = this.GetMemberAndClubRelations(memberAndClubRelation.MemberID).FirstOrDefault();
                info.ClubID = memberAndClubRelation.ClubID;
                info.MemberID = memberAndClubRelation.MemberID;
                info.IsCaption = memberAndClubRelation.IsCaption;
                this.DataSource.SaveChanges();
            }

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

        public void BatchDeleteMemberAndClubRelation(List<MemberAndClubRelation> memberAndClubRelation) 
        {
            foreach (MemberAndClubRelation item in memberAndClubRelation) 
            {
                this.DataSource.MemberAndClubRelations.Remove(item);
            }
            this.DataSource.SaveChanges();
        }
        public void BatchAddMemberAndClubRelation(List<MemberAndClubRelation> memberAndClubRelation) 
        {
            foreach (MemberAndClubRelation item in memberAndClubRelation)
            {
                this.DataSource.MemberAndClubRelations.Add(item);
            }
            this.DataSource.SaveChanges();
        }
    }
}
