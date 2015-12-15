using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class BookInfoDataProvider : IBookInfoDataProvider
    {
        private DBSource DataSource;

        public BookInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<BookInfo> GetBookList()
        {
            return this.DataSource.BookInfos;
        }

        public BookInfo GetBookInfoByID(long ID)
        {
            BookInfo book = this.DataSource.BookInfos.FirstOrDefault(u => u.ID == ID);
            return book;
        }

        public IEnumerable<BookInfo> GetBookList(BookInfoCondition condition)
        {
            IEnumerable<BookInfo> bookList = this.DataSource.BookInfos;

            if (condition.CategoryID > 0)
            {
                var bcr = this.DataSource.BookAndCategoryRelation;
                var categorys = this.DataSource.CategoryInfos;
                bookList = from r in bcr
                           join b in bookList on r.Book_ID equals b.ID
                           join c in categorys on r.Category_ID equals c.ID
                           where c.ID == condition.CategoryID
                           select b;
            }

            if (!String.IsNullOrEmpty(condition.BookName))
            {
                bookList = bookList.Where(b => b.BookName.Contains(condition.BookName));
            }

            return bookList;
        }

        public void Add(BookInfo entity)
        {
            this.DataSource.BookInfos.Add(entity);
            this.DataSource.SaveChanges();
        }

        public void Update(BookInfo entity)
        {
            BookInfo book = this.GetBookInfoByID(entity.ID);

            book.BookName = entity.BookName;
            book.ISBN = entity.ISBN;
            book.Publish_Date = entity.Publish_Date;
            book.Avaliable_Inventory = entity.Avaliable_Inventory;
            book.Max_Inventory = entity.Max_Inventory;
            book.Price_Inventory = entity.Price_Inventory;

            book.PublisherInfo = this.DataSource.PublisherInfos.Find(entity.PublisherInfo.ID);

            book.BookAndCategorys = entity.BookAndCategorys;
            book.BookAndAuthors = entity.BookAndAuthors;
           

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            BookInfo book = GetBookInfoByID(id);

            if (book != null)
            {
                this.DataSource.BookInfos.Remove(book);
                this.DataSource.SaveChanges();
            }
        }
    }
}
