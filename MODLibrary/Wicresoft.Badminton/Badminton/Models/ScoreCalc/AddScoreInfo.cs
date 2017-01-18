using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.ScoreCalc
{
    public class AddScoreInfo:BaseViewModel
    {
        public long MemberID
        {
            get;
            set;
        }
        
        public String MemberName
        {
            get;
            set;
        }

        public long Score
        {
            get;
            set;
        }

        public String Comments
        {
            get;
            set;
        }
    }
}