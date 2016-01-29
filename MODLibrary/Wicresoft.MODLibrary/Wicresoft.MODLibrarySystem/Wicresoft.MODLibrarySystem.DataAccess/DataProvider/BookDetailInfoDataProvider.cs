using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class BookDetailInfoDataProvider : IBookDetailInfoDataProvider
    {
        private DBSource DataSource;

        public BookDetailInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<BookDetailInfo> GetBookDetailList()
        {
            return this.DataSource.BookDetailInfos;
        }

        public BookDetailInfo GetBookDetailInfoByID(long ID)
        {
            BookDetailInfo bookDetail = this.DataSource.BookDetailInfos.FirstOrDefault(u => u.ID == ID);
            return bookDetail;
        }

        public void Add(BookDetailInfo entity)
        {
            if (entity.BookInfo != null)
            {
                entity.BookInfo = this.DataSource.BookInfos.FirstOrDefault(b => b.ID == entity.BookInfo.ID);
            }

            if (entity.UserInfo != null)
            {
                entity.UserInfo = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }

            this.DataSource.BookDetailInfos.Add(entity);
            this.DataSource.SaveChanges();
        }

        public void Update(BookDetailInfo entity)
        {
            BookDetailInfo bookdetail = this.GetBookDetailInfoByID(entity.ID);

            if (entity.BookInfo != null)
            {
                entity.BookInfo = this.DataSource.BookInfos.FirstOrDefault(b => b.ID == entity.BookInfo.ID);
            }

            if (entity.UserInfo != null)
            {
                entity.UserInfo = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }

            bookdetail.Status = entity.Status;
            bookdetail.Storage_Time = entity.Storage_Time;
            bookdetail.CreateTime = entity.CreateTime;

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            BookDetailInfo bookdetail = GetBookDetailInfoByID(id);

            if (bookdetail != null)
            {
                this.DataSource.BookDetailInfos.Remove(bookdetail);
                this.DataSource.SaveChanges();
            }
        }
    }
}
