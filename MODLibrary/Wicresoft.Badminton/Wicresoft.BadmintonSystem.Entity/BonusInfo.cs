using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class BonusInfo : BaseEntity
    {

        public virtual Int32 Score
        {
            get;
            set;
        }

        public virtual Int32 MatchID
        {
            get;
            set;
        }

        public virtual Boolean Updated
        {
            get;
            set;
        }

        [Required]
        public virtual BonusType BonusType
        {
            get;
            set;
        }

        [Required]
        public virtual ChampionshipInfo ChampionID
        {
            get;
            set;
        }

        [Required]
        public virtual MemberInfo MemberID
        {
            get;
            set;
        }
    }
}
