using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class MemberInfo : BaseEntity
    {
        [Required]
        public virtual String Name
        {
            get;
            set;
        }

        public virtual Boolean Male
        {
            get;
            set;
        }

        public virtual DateTime UpdateDate
        {
            get;
            set;
        }

        public virtual Int32 Score
        {
            get;
            set;
        }

        public virtual Int32 TestScore
        {
            get;
            set;
        }

       
    }
}
