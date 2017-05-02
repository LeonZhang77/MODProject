using Badminton.Models.MatchInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badminton.Models.MatchVerify
{
    public class MatchVerifyIndexModel
    {
        public MatchVerifyIndexModel()
        {
            this.WaitingForScoreList = new List<MatchModel>();
            this.WaitingForVerifyList = new List<MatchVerifyModel>();
            this.ChampionshipList = new List<SelectListItem>();
            this.ChampionshipCompetingTypeList = new List<SelectListItem>();
            this.CompetingList = new List<SelectListItem>();
            this.SearchMemberList = new List<SelectListItem>();
            this.SearchMaleMemberList = new List<SelectListItem>();
            this.SearchFemaleMemberList = new List<SelectListItem>();
            this.MatchTypeList = new List<SelectListItem>();
            this.TipMessage = null;
        }
        public string TipMessage
        {
            get;
            set;
        }
        public List<MatchVerifyModel> WaitingForVerifyList
        {
            get;
            set;
        }

        public List<MatchModel> WaitingForScoreList
        {
            get;
            set;
        }


        public List<SelectListItem> ChampionshipList
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

        public List<SelectListItem> MatchTypeList
        {
            get;
            set;
        }
    }
}