using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Models.FinalScoreManage
{
    public class ParametersModel
    {
        public ParametersModel()
        {
            this.ChampionshipList = new List<SelectListItem>();
            this.SearchMemberList = new List<SelectListItem>();

            this.DateRange1 = 6;
            this.Rate1 = 1;

            this.DateRange2 = 12;
            this.Rate2 = 0.9;

            this.DateRange3 = 18;
            this.Rate3 = 0.75;

            this.DateRange4 = 24;
            this.Rate4 = 0.6;

            this.Rate5 = 0.45;

            this.ActionSteps = 0;
        }

        public List<SelectListItem> ChampionshipList
        {
            get;
            set;
        }
        public List<SelectListItem> SearchMemberList
        {
            get;
            set;
        }
        public string ChampionshipID
        {
            get;
            set;
        }

        public string MemberID
        {
            get;
            set;
        }

        public long Score
        {
            get;
            set;
        }

        public long DateRange1
        {
            get;
            set;
        }

        public long DateRange2
        {
            get;
            set;
        }

        public long DateRange3
        {
            get;
            set;
        }

        public long DateRange4
        {
            get;
            set;
        }

        public double Rate1
        {
            get;
            set;
        }

        public double Rate2
        {
            get;
            set;
        }

        public double Rate3
        {
            get;
            set;
        }

        public double Rate4
        {
            get;
            set;
        }

        public double Rate5
        {
            get;
            set;
        }

        public DateTime StandDate
        {
            get;
            set;
        }

        public long ActionSteps
        {
            get;
            set;
        }
    }

    
    
}