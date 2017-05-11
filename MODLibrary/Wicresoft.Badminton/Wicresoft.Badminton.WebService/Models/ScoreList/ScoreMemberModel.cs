using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.BadmintonSystem.WebService.Models
{
    public class ScoreMemberModel:BaseViewModel
    {
        public String MemberName
        {
            get;
            set;
        }

        public bool Male
        {
            get;
            set;
        }
        public long Score
        {
            get;
            set;
        }

        public double WinRate
        {
            get;
            set;
        }

        public double AverageWinRate
        {
            get;
            set;
        }
        public DateTime LastUpdateDate
        {
            get;
            set;
        }
        public long Ranking
        {
            get;
            set;
        }
    }
}