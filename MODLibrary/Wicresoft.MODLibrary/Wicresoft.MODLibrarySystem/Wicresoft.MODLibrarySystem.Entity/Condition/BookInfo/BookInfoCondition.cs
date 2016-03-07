using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem;

namespace Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo
{
    public class BookInfoCondition : BaseCondition
    {
        public String BookName
        {
            get;
            set;
        }

        public long CategoryID
        {
            get;
            set;
        }

        public Entity.PublisherInfo Publisher
        {
            get;
            set;
        }

        public Boolean? IsAvaliable
        {
            get;
            set;
        }
    }
}
