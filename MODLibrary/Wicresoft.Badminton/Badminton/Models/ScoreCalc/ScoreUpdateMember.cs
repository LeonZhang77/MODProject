using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.ScoreCalc
{
    public class ScoreUpdateMember:BaseViewModel
    {
        public String MemberName
        {
            get;
            set;
        }

        public long OriginalScore
        {
            get;
            set;
        }

        public long UpdateScore
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