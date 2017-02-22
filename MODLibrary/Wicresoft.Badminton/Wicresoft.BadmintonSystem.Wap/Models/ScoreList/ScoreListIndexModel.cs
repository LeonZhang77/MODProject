using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wicresoft.BadmintonSystem.Wap.Models.ScoreList
{
    public class ScoreListIndexModel
    {

        public ScoreListIndexModel()
        {
            this.ScoreList = new List<ScoreMemberModel>();
        }
        
       
        public List<ScoreMemberModel> ScoreList
        {
            get;
            set;
        }

        public String SearchSelectedID
        {
            get;
            set;
        }
        public List<SelectListItem> SearchMemberList
        {
            get;
            set;
        }
    }
}