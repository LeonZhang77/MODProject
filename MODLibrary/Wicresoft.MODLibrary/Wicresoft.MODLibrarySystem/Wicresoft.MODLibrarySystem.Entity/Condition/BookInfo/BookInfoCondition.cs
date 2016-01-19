using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
