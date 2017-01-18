using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.ScoreCalc
{
    public class AddBonusInfo:BaseViewModel
    {
        public long MemberID
        {
            get;
            set;
        }
        
        public String MembernName
        {
            get;
            set;
        }

        public long ChampionID
        {
            get;
            set;
        }

        public String ChampionTitle
        {
            get;
            set;
        }

        public long BonusTypeID
        {
            get;
            set;
        }

        public String BonusTypeDescription
        {
            get;
            set;
        }
        public long Score
        {
            get;
            set;
        }


    }
}