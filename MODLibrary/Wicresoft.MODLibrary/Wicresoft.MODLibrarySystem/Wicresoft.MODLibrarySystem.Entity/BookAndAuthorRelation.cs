using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class BookAndAuthorRelation
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("BookInfo")]
        public long Book_ID
        {
            get;
            set;
        }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("AuthorInfo")]
        public long Author_ID
        {
            get;
            set;
        }

        public virtual Int32 OrderIndex
        {
            get;
            set;
        }

        public virtual BookInfo BookInfo
        {
            get;
            set;
        }

        public virtual AuthorInfo AuthorInfo
        {
            get;
            set;
        }
    }
}
