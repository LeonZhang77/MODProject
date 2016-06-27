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
    public class WaitingForTakeModel : BaseViewModel
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

        public String Email
        {
            get;
            set;
        }
        public String Floor
        {
            get;
            set;
        }

        public ProcessRecord GetTakeEntity(out BorrowAndReturnRecordInfo borrowAndReturnRecordInfo,UserInfo user)
        {
            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
            borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(this.ID);

            ProcessRecord processInfo = new ProcessRecord();
            processInfo.Status = RentRecordStatus.Taked;
            processInfo.UserInfo = user;
            processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
            processInfo.Comments = "";

            borrowAndReturnRecordInfo.Status = RentRecordStatus.Taked;
            borrowAndReturnRecordInfo.Borrow_Date = DateTime.Today;
            borrowAndReturnRecordInfo.Forcast_Date = DateTime.Today.AddDays(30);
            
            return processInfo;
        }

        public ProcessRecord GetRevokeEntity(out BorrowAndReturnRecordInfo borrowAndReturnRecordInfo,
                                                                                out BookDetailInfo bookDetailInfo,
                                                                                    out BookModel bookModel, UserInfo user)
        {
            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
            IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
            borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(this.ID);
            bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
            BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);
            
            ProcessRecord processInfo = new ProcessRecord();
            processInfo.UserInfo = user;
            processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
            processInfo.Status = RentRecordStatus.Revoked;
            processInfo.Comments = "You didn't take your book in limited time.";

            borrowAndReturnRecordInfo.Status = RentRecordStatus.Revoked;

            bookDetailInfo.Status = BookStatus.InStore;

            bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory + 1;
            bookModel = BookModel.GetViewModel(bookInfo);

            return processInfo;
        }        
    }
}
