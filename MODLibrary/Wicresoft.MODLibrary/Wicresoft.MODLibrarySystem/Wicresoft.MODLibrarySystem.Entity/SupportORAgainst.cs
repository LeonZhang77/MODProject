using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
   public class SupportORAgainst : BaseEntity
    {
       public SupportORAgainst()
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
       public virtual BookInfo BookInfo
       {
           get;
           set;
       }

       [Required]
       public virtual SupportAgainstStatus Status
       {
           get;
           set;
       }
    }
}
