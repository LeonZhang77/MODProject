using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badminton.Models.ScoreManage.InitialScore
{
    public class InitialScoreIndex
    {

        public InitialScoreIndex()
        {
            this.MemberList = new List<InitialScoreMember>();
        }

        public List<InitialScoreMember> MemberList
        {
            get;
            set;
        }       
        
    }
}