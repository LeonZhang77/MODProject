using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class AuthorInfo : BaseEntity
    {
        public AuthorInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual String AuthorName
        {
            get;
            set;
        }

        public virtual String AuthorIntroduction
        {
            get;
            set;
        }

        public virtual ICollection<BookAndAuthorRelation> BookAndAuthors
        {
            get;
            set;
        }
    }
}
