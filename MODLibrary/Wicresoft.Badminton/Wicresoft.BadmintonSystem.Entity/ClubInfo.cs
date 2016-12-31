using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class ClubInfo : BaseEntity
    {
        [Required]
        public virtual String Name
        {
            get;
            set;
        }

        public virtual String Description
        {
            get;
            set;
        }
    }
}
