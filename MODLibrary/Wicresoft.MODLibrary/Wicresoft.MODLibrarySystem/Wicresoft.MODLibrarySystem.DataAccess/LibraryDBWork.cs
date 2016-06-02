using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess
{
    public sealed class LibraryDBWork : ILibraryDBWork
    {
        private readonly DBSource entities;

        public LibraryDBWork()
        {
            this.entities=new DBSource();
        }

        public IDBStore<AuthorInfo> AuthorInfos
        {
            get { return new DBStore<AuthorInfo>(this.entities); }
        }

        public IDBStore<BookInfo> BookInfos
        {
            get { return new DBStore<BookInfo>(this.entities); }
        }

        public IDBStore<BookDetailInfo> BookDetailInfos
        {
            get { return new DBStore<BookDetailInfo>(this.entities); }
        }

        public IDBStore<BookAndCategoryRelation> BookAndCategoryRelations
        {
            get { return new DBStore<BookAndCategoryRelation>(this.entities); }
        }

        public IDBStore<BookAndAuthorRelation> BookAndAuthorRelations
        {
            get { return new DBStore<BookAndAuthorRelation>(this.entities); }
        }

        public IDBStore<BorrowAndReturnRecordInfo> BorrowAndReturnRecordInfos
        {
            get { return new DBStore<BorrowAndReturnRecordInfo>(this.entities); }
        }

        public IDBStore<CategoryInfo> CategoryInfos
        {
            get { return new DBStore<CategoryInfo>(this.entities); }
        }

        public IDBStore<PublisherInfo> PublisherInfos
        {
            get { return new DBStore<PublisherInfo>(this.entities); }
        }

        public IDBStore<UserInfo> UserInfos
        {
            get { return new DBStore<UserInfo>(this.entities); }
        }

        public int Save()
        {
            return this.entities.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
