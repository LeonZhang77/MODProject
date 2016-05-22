using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wicresoft.MODLibrarySystem.WebUI.Models.RentManage;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;


namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class RentManageController : BaseController
    {
        public IBookInfoDataProvider IBookInfoDataProvider;
        public IBookDetailInfoDataProvider IBookDetailInfoDataProvider;
        public IBorrowAndReturnRecordInfoDataProvider IBorrowAndReturnRecordInfoDataProvider;
        public IProcessRecordDataProvider IProcessRecordDataProvider;
        public RentManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
            this.IBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            this.IBorrowAndReturnRecordInfoDataProvider = new BorrowAndReturnRecordInfoDataProvider();
            this.IProcessRecordDataProvider = new ProcessRecordDataProvider();
        }
        
        public ActionResult Index()
        {
            RentManageIndexModel model = new RentManageIndexModel();

            return View(model);
        }
        public ActionResult MyRequest()
        {
            MyRequestModel model = new MyRequestModel();

            return View(model);
        }
        public ActionResult ReadHistory()
        {
            ReadHistoryModel model = new ReadHistoryModel();

            return View(model);
        }

        public ActionResult BookToRent(long id)
        {
            BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
            BookToRentModel model = BookToRentModel.GetViewMode(bookInfo, this.LoginUser());
            return View(model);
        }

        public string RequstBook(string q) 
        {
            try
            {
                int id = Convert.ToInt32(q);
                BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
                BookDetailInfo bookDetailInfo = this.IBookDetailInfoDataProvider.GetAvaliableBookDetailInfoByBookInfoID(id);
                if (bookDetailInfo == null)
                {
                    Exception ex = new Exception("There is no avaliable Book!");
                    throw (ex);
                }
                else 
                {
                    BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                    borrowAndReturnRecordInfo.BookDetailInfo = bookDetailInfo;
                    borrowAndReturnRecordInfo.UserInfo = this.LoginUser();
                    borrowAndReturnRecordInfo.Status = RentRecordStatus.Pending;
                    this.IBorrowAndReturnRecordInfoDataProvider.Add(borrowAndReturnRecordInfo);

                    ProcessRecord processRecord = new ProcessRecord();
                    processRecord.UserInfo = this.LoginUser();
                    processRecord.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                    processRecord.Status = RentRecordStatus.Pending;
                    this.IProcessRecordDataProvider.Add(processRecord);

                    bookDetailInfo.Status = BookStatus.Rent;
                    this.IBookDetailInfoDataProvider.Update(bookDetailInfo);
                    bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory - 1;
                    this.IBookInfoDataProvider.Update(bookInfo);

                }
                //book avaliable -1
                // books udpate status: pending
                //borrowAndReturn, new record, status: pending
                //Process, new record, status: pending
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
            //return "false";
        }
    }
}