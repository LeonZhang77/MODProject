using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long ID
        {
            get;
            set;
        }

        public virtual DateTime CreateTime
        {
            get;
            set;
        }
    }
}
