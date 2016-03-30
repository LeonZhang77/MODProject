using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;


namespace Wicresoft.MODLibrarySystem.WebUI.Models.BookManage
{
    
    public class DetailBookModel : BookModel
    {
        public DetailBookModel()
        {
            this.BookDetailList = new List<DetailBooksModel>();
        }

        public List<DetailBooksModel> BookDetailList
        {
            get;
            set;
        }

        public static DetailBookModel GetViewModel(BookInfo bookInfo, UserInfo userInfo)
        {
            IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            DetailBookModel model = new DetailBookModel();

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

            //ISupportAndObjectionInfoDataProvider dataProvider = new SupportAndObjectionInfoDataProvider();

            //model.IsAvaliableForSupport = dataProvider.GetCount(bookInfo, true, userInfo) >= 1 ? false : true;
            //model.Supports = dataProvider.GetCount(bookInfo, true).ToString();
            //model.Objections = dataProvider.GetCount(bookInfo, false).ToString();

            model.IsAvaliableForSupport = true;
            model.Supports = Convert.ToInt32(3).ToString(); ;
            model.Objections = Convert.ToInt32(5).ToString();

            List<BookDetailInfo> books = iBookDetailInfoDataProvider.GetBookDetailList().Where(b => b.BookInfo.ID == bookInfo.ID).ToList();
            foreach (var item in books)
            {
                model.BookDetailList.Add(DetailBooksModel.GetViewModel(item));
            }

            return model;
        }

    }
}