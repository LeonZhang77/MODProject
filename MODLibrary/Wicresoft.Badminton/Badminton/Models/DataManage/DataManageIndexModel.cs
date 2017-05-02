using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badminton.Models.DataManage
{
    public class DataManageIndexModel
    {
        public DataManageIndexModel()
        {
            this.MemberList = new List<MemberModel>();
            this.ClubList = new List<ClubModel>();
            this.ChampionshipTypeSelectList = new List<SelectListItem>();
            this.CompetingTypeSelectList = new List<SelectListItem>();
            this.MemberWithoutClub = new List<SelectListItem>();
            this.TipMessage = null;
        }

        public String TipMessage 
        {
            get;
            set;
        }

        public List<MemberModel> MemberList
        {
            get;
            set;
        }

        public List<ClubModel> ClubList
        {
            get;
            set;
        }

        public List<ChampionshipModel> ChampionshipList
        {
            get;
            set;
        }

        public List<SelectListItem> ChampionshipTypeSelectList
        {
            get;
            set;
        }

        public List<SelectListItem> CompetingTypeSelectList
        {
            get;
            set;
        }
        public List<SelectListItem> MemberWithoutClub
        {
            get;
            set;
        }

       

    }
}