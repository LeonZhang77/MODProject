using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class SupportAndObjectionInfo : BaseEntity
    {
        public SupportAndObjectionInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual Boolean SupportOrObjection
        {
            get;
            set;
        }

        public virtual BookInfo BookInfo
        {
            get;
            set;
        }

        public virtual UserInfo UserInfo
        {
            get;
            set;
        }
    }
}
