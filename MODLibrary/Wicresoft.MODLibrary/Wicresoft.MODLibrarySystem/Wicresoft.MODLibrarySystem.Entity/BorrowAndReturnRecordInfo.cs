using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class BorrowAndReturnRecordInfo : BaseEntity
    {
        public BorrowAndReturnRecordInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual UserInfo UserInfo
        {
            get;
            set;
        }

        [Required]
        public virtual BookDetailInfo BookDetailInfo
        {
            get;
            set;
        }

        [Required]
        public virtual DateTime Borrow_Date
        {
            get;
            set;
        }

        [Required]
        public virtual DateTime Forcast_Date
        {
            get;
            set;
        }

        public virtual DateTime Return_Date
        {
            get;
            set;
        }

        [Required]
        public virtual RentRecordStatus Status
        {
            get;
            set;
        }
    }
}
