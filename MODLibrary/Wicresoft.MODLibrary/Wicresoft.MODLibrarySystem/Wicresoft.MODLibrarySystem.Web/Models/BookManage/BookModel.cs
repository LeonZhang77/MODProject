using System;
using System.Collections.Generic;
using System.Linq;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.Web.Models.CategroyManage;

namespace Wicresoft.MODLibrarySystem.Web.Models.BookManage
{
    public class BookModel : BaseViewModel
    {
        public BookModel()
        {
            this.ChooseCategoryModelList = new List<EasyCategoryModel>();
            this.BookDetailList = new List<AddDetailBookModel>();
        }
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

        public String PublisherNameValue
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

        public String CatagoryNameValue
        {
            get;
            set;
        }

        public String AuthorName
        {
            get;
            set;
        }

        public String AuthorNameValue
        {
            get;
            set;
        }

        public String AuthorDisplayName
        {
            get;
            set;
        }

        public List<EasyCategoryModel> ChooseCategoryModelList
        {
            get;
            set;
        }

        public List<AddDetailBookModel> BookDetailList
        {
            get;
            set;
        }

        public bool IsUse
        {
            get;
            set;
        }

        public BookInfo GetEntity()
        {
            BookInfo bookInfo = new BookInfo();

            if (this.ID > 0)
            {
                IBookInfoDataProvider IBookInfoDataProvider=new BookInfoDataProvider();
                bookInfo = IBookInfoDataProvider.GetBookInfoByID(this.ID);
            }

            bookInfo.ID = this.ID;
            bookInfo.BookName = this.BookName;
            bookInfo.ISBN = this.ISBN;

            bookInfo.Publish_Date = Convert.ToDateTime(this.Publish_Date);
            bookInfo.Price_Inventory = Decimal.Parse(this.Price_Inventory);

            bookInfo.PublisherInfo = GetPublisherInfo(this.PublisherNameValue);
            bookInfo.BookAndAuthors = GetAuthorInfoRelationList(this.AuthorNameValue, bookInfo);
            bookInfo.BookAndCategorys = GetCategoryInfoRelationList(this.CatagoryNameValue, bookInfo);

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
                model.PublisherNameValue = bookInfo.PublisherInfo.ID.ToString();
            }

            model.Publish_Date = bookInfo.Publish_Date.ToString(UntityContent.IOSDateTemplate);
            model.Avaliable_Inventory = Convert.ToInt32(bookInfo.Avaliable_Inventory).ToString();
            model.Max_Inventory = Convert.ToInt32(bookInfo.Max_Inventory).ToString();
            model.Price_Inventory = bookInfo.Price_Inventory.ToString("F");

            string categoryNameValue = string.Empty;
            model.CatagoryName = GetCategoryName(bookInfo, out categoryNameValue);
            model.CatagoryNameValue = categoryNameValue;

            string displayName = string.Empty;
            string authorNameValue = string.Empty;
            model.AuthorName = GetAuthorName(bookInfo, out displayName, out authorNameValue);
            model.AuthorDisplayName = displayName;
            model.AuthorNameValue = authorNameValue;

            model.IsUse = bookInfo.Max_Inventory > 0 ? true : false;

            return model;
        }

        public ICollection<BookAndCategoryRelation> GetCategoryInfoRelationList(string cIDs, BookInfo book)
        {
            ICollection<BookAndCategoryRelation> list = new List<BookAndCategoryRelation>();
            ICategoryInfoDataProvider categoryDataProvider = new CategoryInfoDataProvider();
            if (!String.IsNullOrEmpty(cIDs))
            {
                var aArrary = cIDs.Split(UntityContent.SplitValueStr);
                foreach (var item in aArrary)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        BookAndCategoryRelation relationEntity = new BookAndCategoryRelation();
                        relationEntity.BookInfo = book;
                        relationEntity.CategoryInfo = categoryDataProvider.GetCategoryByID(long.Parse(item));
                        list.Add(relationEntity);
                    }
                }
            }

            return list;
        }

        public ICollection<BookAndAuthorRelation> GetAuthorInfoRelationList(string aIDs, BookInfo book)
        {
            ICollection<BookAndAuthorRelation> list = new List<BookAndAuthorRelation>();
            IAuthorInfoDataProvider authorDataProvider = new AuthorInfoDataProvider();
            if (!String.IsNullOrEmpty(aIDs))
            {
                var aArrary = aIDs.Split(UntityContent.SplitValueStr);
                foreach (var item in aArrary)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        BookAndAuthorRelation relationEntity = new BookAndAuthorRelation();
                        relationEntity.BookInfo = book;
                        relationEntity.AuthorInfo = authorDataProvider.GetAuthorListByID(long.Parse(item));
                        list.Add(relationEntity);
                    }
                }
            }

            return list;
        }

        public PublisherInfo GetPublisherInfo(string pid)
        {
            PublisherInfo info = null;

            if (!String.IsNullOrEmpty(pid))
            {
                IPublisherInfoDataProvider dataProvider = new PublisherInfoDataProvider();
                info = dataProvider.GetPublisherByID(long.Parse(pid));
            }

            return info;
        }

        public static string GetCategoryName(BookInfo bookInfo, out string categoryNameValue)
        {
            string categoryName = string.Empty;
            string nameValue = string.Empty;

            foreach (var item in bookInfo.BookAndCategorys.OrderBy(b => b.OrderIndex))
            {
                categoryName += item.CategoryInfo.CategoryName + UntityContent.CategoryPathTemplate;
                nameValue += item.CategoryInfo.ID.ToString() + UntityContent.SplitValueStr;
            }

            categoryNameValue = nameValue;
            return categoryName.TrimEnd(UntityContent.CategoryPathTemplate);
        }

        public static string GetAuthorName(BookInfo bookInfo, out string authorDisplayName, out string authorNameValue)
        {
            string authorName = string.Empty;
            string displayName = string.Empty;
            string valueName = string.Empty;
            foreach (var item in bookInfo.BookAndAuthors.OrderBy(a => a.OrderIndex))
            {
                authorName += item.AuthorInfo.AuthorName + UntityContent.AuthorPathTemplate;
                displayName += item.AuthorInfo.AuthorName + UntityContent.SplitDisplayStr;
                valueName += item.AuthorInfo.ID.ToString() + UntityContent.SplitValueStr;
            }

            authorNameValue = valueName;
            authorDisplayName = displayName;
            return authorName.TrimEnd(UntityContent.AuthorPathTemplate);
        }
    }
}