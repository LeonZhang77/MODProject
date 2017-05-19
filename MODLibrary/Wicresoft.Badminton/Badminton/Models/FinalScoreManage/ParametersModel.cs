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

        public long ActionSteps
        {
            get;
            set;
        }
    }

    
    
}