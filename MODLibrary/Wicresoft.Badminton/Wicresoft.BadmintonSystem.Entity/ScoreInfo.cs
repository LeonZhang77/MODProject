using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class ScoreInfo : BaseEntity
    {
        public virtual MemberInfo MemberID
        {
            get;
            set;
        }

        public virtual DateTime CalculateDate
        {
            get;
            set;
        }

        public virtual DateTime PeriodEnd
        {
            get;
            set;
        }

        public virtual Int32 Score
        {
            get;
            set;
        }

        public virtual String Comment
        {
            get;
            set;
        }
    }
}
