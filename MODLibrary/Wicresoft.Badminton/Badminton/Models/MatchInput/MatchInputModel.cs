using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;


namespace Badminton.Models.MatchInput
{
    public class MatchInputModel
    {
        public MatchInputModel()
        {
            this.ChampionshipList = new List<SelectListItem>();
            this.TrueActiveChampionshipList = new List<SelectListItem>();
            this.ChampionshipCompetingTypeList = new List<SelectListItem>();
            this.CompetingList = new List<SelectListItem>();
            this.SearchMemberList = new List<SelectListItem>();
            this.SearchMaleMemberList = new List<SelectListItem>();
            this.SearchFemaleMemberList = new List<SelectListItem>();
            this.TipMessage = null;
            this.ScoreList2=scoreList();
        }
        public string ChampionshipID
        {
            get;
            set;
        }
        public List<SelectListItem> ScoreList2 {
            get;
            set;
        }
        public string TipMessage 
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

        public List<SelectListItem> scoreList()
        {
            List<SelectListItem> score = new List<SelectListItem>();
            for (int i = 0; i <= 25; i++) 
            {
                SelectListItem item = new SelectListItem();
                item.Value = i.ToString();
                item.Text = i.ToString();
                score.Add(item);
            }
            return score;
        }
    }
    
}