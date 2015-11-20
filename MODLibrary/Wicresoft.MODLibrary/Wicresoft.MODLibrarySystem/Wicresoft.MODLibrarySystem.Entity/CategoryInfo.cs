using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class CategoryInfo : BaseEntity
    {
        public CategoryInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        [Required]
        public virtual String CategoryName
        {
            get;
            set;
        }

        public virtual CategoryInfo ParentCategoryInfo
        {
            get;
            set;
        }

        public virtual ICollection<BookAndCategoryRelation> BookAndCategorys
        {
            get;
            set;
        }
    }
}
