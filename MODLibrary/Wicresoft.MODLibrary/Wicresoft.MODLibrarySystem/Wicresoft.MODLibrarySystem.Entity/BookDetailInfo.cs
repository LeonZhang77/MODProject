using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class BookDetailInfo : BaseEntity
    {
        public BookDetailInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual BookInfo BookInfo
        {
            get;
            set;
        }

        [Required]
        public virtual UserInfo UserInfo
        {
            get;
            set;
        }

        [Required]
        public virtual BookStatus Status
        {
            get;
            set;
        }

        [Required]
        public virtual DateTime Storage_Time
        {
            get;
            set;
        }
    }
}
