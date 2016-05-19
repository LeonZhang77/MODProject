using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.UserInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface ISupportORAgainstInfoDataProvider : IBaseDataProvider<SupportORAgainst>
    {
        int GetCount(BookInfo bookInfo, SupportAgainstStatus supportOrAgainst = SupportAgainstStatus.Support, UserInfo userInfo = null);
    }
}
