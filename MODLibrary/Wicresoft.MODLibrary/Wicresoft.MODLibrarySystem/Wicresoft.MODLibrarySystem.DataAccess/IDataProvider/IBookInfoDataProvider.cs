using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IBookInfoDataProvider : IBaseDataProvider<BookInfo>
    {
        IEnumerable<BookInfo> GetBookList();

        IEnumerable<BookInfo> GetBookList(BookInfoCondition condition);
    }
}
