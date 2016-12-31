using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;
using Newtonsoft.Json.Linq;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class UserRequestModel : BaseViewModel
    {
        public string Title
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
        public String Publish
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

        public bool IsRejected
        {
            get;
            set;
        }

        public ProcessRecord GetEntity(UserInfo user, string q, bool RejectOrApprove, out BorrowAndReturnRecordInfo borrowAndReturnRecordInfo, out BookDetailInfo bookDetailInfo, out BookModel bookModel)
        {
            JObject obj = JObject.Parse(q);
            string comments = (string)obj["comments"];
            bool errorOrNot = false;
            if (RejectOrApprove)
            {
                errorOrNot = ((string)obj["isChecked"] == "true") ? true : false;
            }

            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
            IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
            borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(this.ID);
            bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
            BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);

            ProcessRecord processInfo = new ProcessRecord();
            processInfo.UserInfo = user;
            processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
            processInfo.Comments = comments;
            bookModel = BookModel.GetViewModel(bookInfo);

            if (RejectOrApprove)
            {
                processInfo.Status = RentRecordStatus.Rejected;
                borrowAndReturnRecordInfo.Status = RentRecordStatus.Rejected;
                if (errorOrNot)
                {
                    bookDetailInfo.Status = BookStatus.Error;
                    bookInfo.Max_Inventory = bookInfo.Max_Inventory - 1;
                    bookModel.Max_Inventory = bookInfo.Max_Inventory.ToString();
                }
                else
                {
                    bookDetailInfo.Status = BookStatus.InStore;
                    bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory + 1;
                    bookModel.Avaliable_Inventory = bookInfo.Avaliable_Inventory.ToString();
                }
            }
            else
            {
                processInfo.Status = RentRecordStatus.Approved;
                borrowAndReturnRecordInfo.Status = RentRecordStatus.Approved;
            }

            return processInfo;
        }
    }
}
