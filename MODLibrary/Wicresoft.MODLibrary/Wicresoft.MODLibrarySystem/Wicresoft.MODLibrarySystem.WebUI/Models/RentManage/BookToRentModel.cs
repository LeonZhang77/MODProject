using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.WebUI.Models;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.RentManage
{
    public class BookToRentModel: BaseViewModel
    {
        public BookToRentModel()
        {
            
        }

        public String BookName
        {
            get;
            set;
        }

        public String AuthorName
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
        public String ISBN
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

        public String Book_Description
        {
            get;
            set;
        }

        public String Can_Borrow_Count
        {
            get;
            set;
        }

        public String Still_Can_Borrow_Count
        {
            get;
            set;
        }

        public bool Still_Can_Borrow
        {
            get;
            set;
        }

        public static BookToRentModel GetViewMode(BookInfo bookInfo, UserInfo userInfo)
        {
            BookToRentModel model = new BookToRentModel();
            model.ID = bookInfo.ID;
            model.BookName = bookInfo.BookName;

            string displayName = string.Empty;
            string authorNameValue = string.Empty;
            model.AuthorName = BookManage.BookModel.GetAuthorName(bookInfo, out displayName, out authorNameValue);
            model.AuthorName = displayName;

            model.ISBN = bookInfo.ISBN;

            if (bookInfo.PublisherInfo != null)
            {
                model.PublisherName = bookInfo.PublisherInfo.PublisherName;                
            }
            model.Publish_Date = bookInfo.Publish_Date.ToString(UntityContent.IOSDateTemplate);
            model.Avaliable_Inventory = Convert.ToInt32(bookInfo.Avaliable_Inventory).ToString();
            model.Max_Inventory = Convert.ToInt32(bookInfo.Max_Inventory).ToString();
            model.Book_Description = "Waiting for DB......";

            int canBorrowCount =  Unity.Helper.DataUnity.GetCanBorrowCount(userInfo);
            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProvider =
                new BorrowAndReturnRecordInfoDataProvider();
            int alreadyBorrowedCount = iBorrowAndReturnRecordInfoDataProvider.GetBooksInHandCount(userInfo);

            model.Can_Borrow_Count = canBorrowCount.ToString();
            if (alreadyBorrowedCount >= canBorrowCount)
            {
                model.Still_Can_Borrow_Count = Convert.ToInt32(0).ToString();
                model.Still_Can_Borrow = false;
            }
            else
            {
                model.Still_Can_Borrow_Count = (canBorrowCount - alreadyBorrowedCount).ToString();
                model.Still_Can_Borrow = true;
            }
            
            return model;

        }

    }
}