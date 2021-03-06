﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess
{
    public class DBSource : DbContext
    {
        public DbSet<UserInfo> UserInfos
        {
            get;
            set;
        }

        public DbSet<BookInfo> BookInfos
        {
            get;
            set;
        }

        public DbSet<CategoryInfo> CategoryInfos
        {
            get;
            set;
        }

        public DbSet<BookAndCategoryRelation> BookAndCategoryRelation
        {
            get;
            set;
        }

        public DbSet<AuthorInfo> AuthorInfos
        {
            get;
            set;
        }

        public DbSet<BookAndAuthorRelation> BookAndAuthorRelation
        {
            get;
            set;
        }

        public DbSet<BookDetailInfo> BookDetailInfos
        {
            get;
            set;
        }

        public DbSet<BorrowAndReturnRecordInfo> BorrowAndReturnRecordInfos
        {
            get;
            set;
        }

        public DbSet<PublisherInfo> PublisherInfos
        {
            get;
            set;
        }

        public DbSet<DelayRecord> DelayRecords
        {
            get;
            set;
        }

        public DbSet<ProcessRecord> ProcessRecords
        {
            get;
            set;
        }

        public DbSet<SupportORAgainst> SupportORAgainstInfos
        {
            get;
            set;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}
