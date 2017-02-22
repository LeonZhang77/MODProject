using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;


namespace Wicresoft.BadmintonSystem.Wap.Models.MatchInput
{
    public class MatchInputModel
    {
        IBadmintionDataProvider provider;
        public MatchInputModel()
        {
            this.provider = new BadmintionDataProvider();
            this.ChampionshipList = new List<SelectListItem>();
            this.TrueActiveChampionshipList = new List<SelectListItem>();
            this.ChampionshipCompetingTypeList = new List<SelectListItem>();
            this.CompetingList = new List<SelectListItem>();
            this.SearchMemberList = new List<SelectListItem>();
            this.SearchMaleMemberList = new List<SelectListItem>();
            this.SearchFemaleMemberList = new List<SelectListItem>();
        }
        public string ChampionshipID
        {
            get;
            set;
        }

        public string CompetingTypeID
        {
            get;
            set;
        }

        public string WinnerPoints
        {
            get;
            set;
        }

        public string LoserPoints
        {
            get;
            set;
        }

        public string MatchDate
        {
            get;
            set;
        }
        public string InputPersonID
        {
            get;
            set;
        }

        public bool SingleOrDouble
        {
            get;
            set;
        }

        public long MaleFemaleOrMixed
        {
            get;
            set;
        }

        public string Winner1ID
        {
            get;
            set;
        }
        public string Winner2ID
        {
            get;
            set;
        }
        public string Loser1ID
        {
            get;
            set;
        }
        public string Loser2ID
        {
            get;
            set;
        }

        public List<SelectListItem> ChampionshipList
        {
            get;
            set;
        }

        public List<SelectListItem> TrueActiveChampionshipList
        {
            get;
            set;
        }

        public List<SelectListItem> CompetingList
        {
            get;
            set;
        }
        public List<SelectListItem> SearchMemberList
        {
            get;
            set;
        }

        public List<SelectListItem> SearchMaleMemberList
        {
            get;
            set;
        }

        public List<SelectListItem> SearchFemaleMemberList
        {
            get;
            set;
        }

        public List<SelectListItem> ChampionshipCompetingTypeList
        {
            get;
            set;
        }        

        public MatchInfo GetEntity()
        {
            MatchInfo info = new MatchInfo();
            info.ChampionID = provider.GetChampionshipInfoByID(Convert.ToInt32(this.ChampionshipID));
            //info.InputPersonID = provider.GetMemberInfoByID(Convert.ToInt32(this.InputPersonID));
            info.MatchDate = DateTime.Parse(this.MatchDate);
            info.WinnerID = provider.GetMemberInfoByID(Convert.ToInt32(this.Winner1ID));
            info.LoserID = provider.GetMemberInfoByID(Convert.ToInt32(this.Loser1ID));
            if(info.ChampionID.CompetingType.Equals(CompetingType.MaleDou)||
                info.ChampionID.CompetingType.Equals(CompetingType.FemaleDou) ||
                info.ChampionID.CompetingType.Equals(CompetingType.MixDou))
            {
                info.WinnerID2 = provider.GetMemberInfoByID(Convert.ToInt32(this.Winner2ID));
                info.LoserID2 = provider.GetMemberInfoByID(Convert.ToInt32(this.Loser2ID));
            }
            info.CreateTime = DateTime.Now;
            return info;
        }
    }
    
}