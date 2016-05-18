using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class BookInfo : BaseEntity
    {
        public BookInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual String BookName
        {
            get;
            set;
        }

        [Required]
        public virtual String ISBN
        {
            get;
            set;
        }

        public virtual PublisherInfo PublisherInfo
        {
            get;
            set;
        }

        [Required]
        public virtual DateTime Publish_Date
        {
            get;
            set;
        }

        public virtual Decimal Avaliable_Inventory
        {
            get;
            set;
        }

        [Required]
        public virtual Decimal Max_Inventory
        {
            get;
            set;
        }

        [Required]
        public virtual Decimal Price_Inventory
        {
            get;
            set;
        }

        [MaxLength(1024 * 1024)]
        public virtual String Summary
        {
            get;
            set;
        }

        public virtual ICollection<BookAndCategoryRelation> BookAndCategorys
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
