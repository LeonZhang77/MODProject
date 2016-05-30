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

            ISupportORAgainstInfoDataProvider dataProvider = new SupportORAgainstInfoDataProvider();
            model.IsAvaliableForSupport = dataProvider.GetCountByUser(bookInfo, userInfo) >= 1 ? false : true;
            model.Supports = dataProvider.GetCountByStatus(bookInfo, SupportAgainstStatus.Support).ToString();
            model.Objections = dataProvider.GetCountByStatus(bookInfo, SupportAgainstStatus.Against).ToString();
            
            return model;
        }

    }
}