using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Web.Models.BookManage
{
    public class BookModel : BaseViewModel
    {
        public String BookName
        {
            get;
            set;
        }
        public String ISBN
        {
            get;
            set;
        }

        public String PublisherName
        {
            get;
            set;
        }

        public String Publish_Date
        {
            get;
            set;
        }

        public String Avaliable_Inventory
        {
            get;
            set;
        }

        public String Max_Inventory
        {
            get;
            set;
        }

        public String Price_Inventory
        {
            get;
            set;
        }

        public String CatagoryName
        {
            get;
            set;
        }

        public String AuthorName
        {
            get;
            set;
        }

        public BookInfo GetEntity()
        {
            BookInfo bookInfo = new BookInfo();

            bookInfo.ID = this.ID;
            bookInfo.BookName = this.BookName;



            return bookInfo;
        }

        public static BookModel GetViewModel(BookInfo bookInfo)
        {
            BookModel model = new BookModel();

            model.ID = bookInfo.ID;
            model.BookName = bookInfo.BookName;
            model.ISBN = bookInfo.ISBN;

            if (bookInfo.PublisherInfo != null)
            {
                model.PublisherName = bookInfo.PublisherInfo.PublisherName;
            }

            model.Publish_Date = bookInfo.Publish_Date.ToString(UntityContent.IOSDateTemplate);
            model.Avaliable_Inventory = bookInfo.Avaliable_Inventory.ToString("F");
            model.Max_Inventory = bookInfo.Max_Inventory.ToString("F");
            model.Price_Inventory = bookInfo.Price_Inventory.ToString("F");

            model.CatagoryName = GetCategoryName(bookInfo);
            model.AuthorName = GetAuthorName(bookInfo);
            
            return model;
        }

        public static string GetCategoryName(BookInfo bookInfo)
        {
            string categoryName = string.Empty;

            foreach (var item in bookInfo.BookAndCategorys.OrderBy(b => b.OrderIndex))
            {
                categoryName += item.CategoryInfo.CategoryName + UntityContent.CategoryPathTemplate;
            }

            return categoryName.TrimEnd(UntityContent.CategoryPathTemplate);
        }

        public static string GetAuthorName(BookInfo bookInfo)
        {
            string authorName = string.Empty;

            foreach (var item in bookInfo.BookAndAuthors.OrderBy(a => a.OrderIndex))
            {
                authorName += item.AuthorInfo.AuthorName + UntityContent.AuthorPathTemplate;
            }

            return authorName.TrimEnd(UntityContent.AuthorPathTemplate);
        }
    }
}