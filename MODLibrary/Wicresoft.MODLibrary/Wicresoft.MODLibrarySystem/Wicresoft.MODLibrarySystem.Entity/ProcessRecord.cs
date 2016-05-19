using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class ProcessRecord : BaseEntity
    {
        public ProcessRecord()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual BorrowAndReturnRecordInfo BorrowAndReturnRecordInfo
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
        public virtual RentRecordStatus Status
        {
            get;
            set;
        }

        [MaxLength(1024)]
        public virtual String Comments
        {
            get;
            set;
        }
    }
}
