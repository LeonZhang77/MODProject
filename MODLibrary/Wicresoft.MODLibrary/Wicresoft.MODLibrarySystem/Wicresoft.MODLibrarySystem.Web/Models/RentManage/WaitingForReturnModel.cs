using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class WaitingForReturnModel : BaseViewModel
    {
        public String Title
        {
            get;
            set;
        }
        public String UserName
        {
            get;
            set;
        }

        public string ExpirationDay
        {
            get;
            set;
        }

        public string Delay
        {
            get;
            set;
        }

        public ProcessRecord GetEntity(UserInfo user, out BorrowAndReturnRecordInfo borrowAndReturnRecordInfo, out BookDetailInfo bookDetailInfo, out BookModel bookModel)
        {
            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
            IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();

            borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(this.ID);
            bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
            BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);

            ProcessRecord processInfo = new ProcessRecord();
            processInfo.Status = RentRecordStatus.Returned;
            processInfo.UserInfo = user;
            processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
            processInfo.Comments = "";

            borrowAndReturnRecordInfo.Status = RentRecordStatus.Returned;
            borrowAndReturnRecordInfo.Return_Date = DateTime.Today;

            bookDetailInfo.Status = BookStatus.InStore;

            bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory + 1;
            bookModel = BookModel.GetViewModel(bookInfo);

            return processInfo;
        }
    }
}
