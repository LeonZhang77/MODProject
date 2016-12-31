using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class MatchInfo : BaseEntity
    {
        public virtual DateTime MatchDate
        {
            get;
            set;
        }

        public virtual Int32 WinnerPoints
        {
            get;
            set;
        }

        public virtual Int32 LoserPoints
        {
            get;
            set;
        }

        public virtual Int32 Compensation
        {
            get;
            set;
        }

        public virtual Boolean Updated
        {
            get;
            set;
        }

        public virtual IgnoreType Ignore
        {
            get;
            set;
        }

        public virtual ChampionshipInfo ChampionID
        {
            get;
            set;
        }        

        public virtual MemberInfo WinnerID
        {
            get;
            set;
        }

        public virtual MemberInfo WinnerID2
        {
            get;
            set;
        }

        public virtual MemberInfo LoserID
        {
            get;
            set;
        }

        public virtual MemberInfo LoserID2
        {
            get;
            set;
        }
    }
}
