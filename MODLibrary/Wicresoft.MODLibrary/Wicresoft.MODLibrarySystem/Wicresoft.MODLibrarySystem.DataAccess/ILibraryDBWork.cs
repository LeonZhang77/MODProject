using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess
{
    public interface ILibraryDBWork : IDisposable
    {
        IDBStore<AuthorInfo> AuthorInfos { get; }

        IDBStore<BookInfo> BookInfos { get; }

        IDBStore<BookDetailInfo> BookDetailInfos { get; }

        IDBStore<BookAndCategoryRelation> BookAndCategoryRelations { get; }

        IDBStore<BookAndAuthorRelation> BookAndAuthorRelations { get; }

        IDBStore<BorrowAndReturnRecordInfo> BorrowAndReturnRecordInfos { get; }

        IDBStore<CategoryInfo> CategoryInfos { get; }

        IDBStore<PublisherInfo> PublisherInfos { get; }

        IDBStore<UserInfo> UserInfos { get; }

        int Save();
    }
}
