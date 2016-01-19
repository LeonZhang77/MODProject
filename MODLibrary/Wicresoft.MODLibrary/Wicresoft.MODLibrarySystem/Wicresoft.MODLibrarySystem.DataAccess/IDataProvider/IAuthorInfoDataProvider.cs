using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.AuthorInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IAuthorInfoDataProvider : IBaseDataProvider<AuthorInfo>
    {
        IEnumerable<AuthorInfo> GetAuthorList();

        IEnumerable<AuthorInfo> GetAuthorList(AuthorInfoCondition authorcondition);

        AuthorInfo GetAuthorListByID(long ID);
    }
}
