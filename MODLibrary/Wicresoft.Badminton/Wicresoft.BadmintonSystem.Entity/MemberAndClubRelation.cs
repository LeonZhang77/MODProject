using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class MemberAndClubRelation : BaseEntity
    {
        [Required]
        public virtual MemberInfo MemberID
        {
            get;
            set;
        }

        [Required]
        public virtual ClubInfo ClubID
        {
            get;
            set;
        }

        public Boolean IsCaption
        {
            get;
            set;
        }

    }
}
