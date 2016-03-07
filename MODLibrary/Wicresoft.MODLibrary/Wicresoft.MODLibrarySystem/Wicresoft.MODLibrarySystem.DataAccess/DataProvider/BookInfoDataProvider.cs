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

            if (condition.Publisher != null)
            {
                bookList = bookList.Where(b => b.PublisherInfo.ID == condition.Publisher.ID);
            }

            if (condition.IsAvaliable.HasValue)
            {
                if (condition.IsAvaliable.Value)
                {
                    bookList = bookList.Where(b => b.Avaliable_Inventory > 0);
                }
            }

            bookList = bookList.OrderByDescending(b => b.Publish_Date);
            
            return bookList;
        }

        public void Add(BookInfo entity)
        {
            ICollection<BookAndCategoryRelation> tempBookAndCategorys = entity.BookAndCategorys;
            ICollection<BookAndAuthorRelation> tempBookAndAuthors = entity.BookAndAuthors;

            if (entity.PublisherInfo != null)
            {
                entity.PublisherInfo = this.DataSource.PublisherInfos.Find(entity.PublisherInfo.ID);
            }

            entity.BookAndAuthors = null;
            entity.BookAndCategorys = null;
            this.DataSource.BookInfos.Add(entity);
            this.DataSource.SaveChanges();

            RefreshBookRelationEntity(entity, tempBookAndCategorys, tempBookAndAuthors);
        }

        public void Update(BookInfo entity)
        {
            ICollection<BookAndCategoryRelation> tempBookAndCategorys = entity.BookAndCategorys;
            ICollection<BookAndAuthorRelation> tempBookAndAuthors = entity.BookAndAuthors;

            BookInfo book = this.GetBookInfoByID(entity.ID);

            book.BookName = entity.BookName;
            book.ISBN = entity.ISBN;
            book.Publish_Date = entity.Publish_Date;
            book.Price_Inventory = entity.Price_Inventory;

            book.Avaliable_Inventory = entity.Avaliable_Inventory;
            book.Max_Inventory = entity.Max_Inventory;

            if (entity.PublisherInfo != null)
            {
                book.PublisherInfo = this.DataSource.PublisherInfos.Find(entity.PublisherInfo.ID);
            }
            else
            {
                book.PublisherInfo = null;
            }

            this.DataSource.SaveChanges();


            RemoveBookRelationEntity(book);

            RefreshBookRelationEntity(book,tempBookAndCategorys,tempBookAndAuthors);
        }

        private void RemoveBookRelationEntity(BookInfo book)
        {
            this.DataSource.BookAndAuthorRelation.RemoveRange(this.DataSource.BookAndAuthorRelation.Where(r => r.Book_ID == book.ID));
            this.DataSource.BookAndCategoryRelation.RemoveRange(this.DataSource.BookAndCategoryRelation.Where(r => r.Book_ID == book.ID));
            this.DataSource.SaveChanges();
        }

        private void RefreshBookRelationEntity(BookInfo book, ICollection<BookAndCategoryRelation> tempBookAndCategorys, ICollection<BookAndAuthorRelation> tempBookAndAuthors)
        {
            if (tempBookAndAuthors != null)
            {
                foreach (var item in tempBookAndAuthors)
                {
                    this.DataSource.BookAndAuthorRelation.Add(new BookAndAuthorRelation { Book_ID = book.ID, Author_ID = item.AuthorInfo.ID });
                }
                this.DataSource.SaveChanges();
            }
            if (tempBookAndCategorys != null)
            {
                foreach (var item in tempBookAndCategorys)
                {
                    this.DataSource.BookAndCategoryRelation.Add(new BookAndCategoryRelation { Book_ID = book.ID, Category_ID = item.CategoryInfo.ID });
                }
                this.DataSource.SaveChanges();
            }
        }

        public void DeleteByID(long id)
        {
            BookInfo book = GetBookInfoByID(id);

            if (book != null)
            {
                RemoveBookRelationEntity(book);

                this.DataSource.BookInfos.Remove(book);
                this.DataSource.SaveChanges();
            }
        }
    }
}
