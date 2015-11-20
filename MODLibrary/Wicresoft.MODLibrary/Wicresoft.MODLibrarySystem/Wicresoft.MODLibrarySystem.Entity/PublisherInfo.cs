using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class PublisherInfo : BaseEntity
    {
        public PublisherInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual String PublisherName
        {
            get;
            set;
        }

        public virtual String PublisherIntroduction
        {
            get;
            set;
        }
    }
}
