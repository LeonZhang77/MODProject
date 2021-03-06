﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Wap.Models.BookManage
{
    public class BookModel : BaseViewModel
    {
        public BookModel()
        {

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

        public String PublisherDisplayDescription
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
        public bool IsAvaliableForSupport
        {
            get;
            set;
        }
        public String Supports
        {
            get;
            set;
        }

        public String Objections
        {
            get;
            set;
        }
        public bool IsAvaliable
        {
            get;
            set;
        }

        public static BookModel GetViewModel(BookInfo bookInfo, UserInfo userInfo)
        {
            BookModel model = new BookModel();

            model.ID = bookInfo.ID;
            model.BookName = bookInfo.BookName;
            model.ISBN = bookInfo.ISBN;

            if (bookInfo.PublisherInfo != null)
            {
                model.PublisherName = bookInfo.PublisherInfo.PublisherName;
                model.PublisherNameValue = bookInfo.PublisherInfo.ID.ToString();
                model.PublisherDisplayDescription = bookInfo.PublisherInfo.PublisherIntroduction;
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

            model.IsAvaliable = bookInfo.Avaliable_Inventory > 0 ? true : false;

            ISupportORAgainstInfoDataProvider dataProvider = new SupportORAgainstInfoDataProvider();
            model.IsAvaliableForSupport = dataProvider.GetCountByUser(bookInfo, userInfo) >= 1 ? false : true;
            model.Supports = dataProvider.GetCountByStatus(bookInfo, SupportAgainstStatus.Support).ToString();
            model.Objections = dataProvider.GetCountByStatus(bookInfo, SupportAgainstStatus.Against).ToString();

            return model;
        }

        public BookInfo GetEntity()
        {
            BookInfo bookInfo = new BookInfo();

            BookInfoDataProvider bookInfoDataProvider = new BookInfoDataProvider();
            IBookInfoDataProvider iBookInfoDataProvider = bookInfoDataProvider;

            if (this.ID > 0)
            {

                bookInfo = iBookInfoDataProvider.GetBookInfoByID(this.ID);
            }

            bookInfo.ID = this.ID;
            bookInfo.BookName = this.BookName;
            bookInfo.ISBN = this.ISBN;

            bookInfo.Publish_Date = Convert.ToDateTime(this.Publish_Date);
            bookInfo.Price_Inventory = Decimal.Parse(this.Price_Inventory);
            bookInfo.Avaliable_Inventory = Convert.ToInt32(Convert.ToDecimal(this.Avaliable_Inventory));
            bookInfo.Max_Inventory = Convert.ToInt32(this.Max_Inventory);

            bookInfo.PublisherInfo = GetPublisherInfo(this.PublisherNameValue, bookInfoDataProvider);
            bookInfo.TempBookAndAuthors = GetAuthorInfoRelationList(this.AuthorNameValue, bookInfo, bookInfoDataProvider);
            bookInfo.TempBookAndCategorys = GetCategoryInfoRelationList(this.CatagoryNameValue, bookInfo, bookInfoDataProvider);

            return bookInfo;
        }

        public ICollection<BookAndCategoryRelation> GetCategoryInfoRelationList(string cIDs, BookInfo book, BookInfoDataProvider bookInfoDataProvider)
        {
            ICollection<BookAndCategoryRelation> list = new List<BookAndCategoryRelation>();
            if (!String.IsNullOrEmpty(cIDs))
            {
                var aArrary = cIDs.Split(UntityContent.SplitValueStr);
                foreach (var item in aArrary)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        long catID = long.Parse(item);
                        BookAndCategoryRelation relationEntity = new BookAndCategoryRelation();
                        relationEntity.BookInfo = book;
                        relationEntity.CategoryInfo = bookInfoDataProvider.DataSource.CategoryInfos.FirstOrDefault(c => c.ID == catID);
                        list.Add(relationEntity);
                    }
                }
            }

            return list;
        }

        public ICollection<BookAndAuthorRelation> GetAuthorInfoRelationList(string aIDs, BookInfo book, BookInfoDataProvider bookInfoDataProvider)
        {
            ICollection<BookAndAuthorRelation> list = new List<BookAndAuthorRelation>();
            if (!String.IsNullOrEmpty(aIDs))
            {
                var aArrary = aIDs.Split(UntityContent.SplitValueStr);
                foreach (var item in aArrary)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        long authID = long.Parse(item);
                        BookAndAuthorRelation relationEntity = new BookAndAuthorRelation();
                        relationEntity.BookInfo = book;
                        relationEntity.AuthorInfo = bookInfoDataProvider.DataSource.AuthorInfos.FirstOrDefault(a => a.ID == authID);
                        list.Add(relationEntity);
                    }
                }
            }

            return list;
        }

        public PublisherInfo GetPublisherInfo(string pid, BookInfoDataProvider bookInfoDataProvider)
        {
            PublisherInfo info = null;

            if (!String.IsNullOrEmpty(pid))
            {
                long publishID = long.Parse(pid);
                info = bookInfoDataProvider.DataSource.PublisherInfos.FirstOrDefault(p => p.ID == publishID);
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