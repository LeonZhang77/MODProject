using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IBookDetailInfoDataProvider:IBaseDataProvider<BookDetailInfo>
    {
        IEnumerable<BookDetailInfo> GetBookDetailList();
    }
}
