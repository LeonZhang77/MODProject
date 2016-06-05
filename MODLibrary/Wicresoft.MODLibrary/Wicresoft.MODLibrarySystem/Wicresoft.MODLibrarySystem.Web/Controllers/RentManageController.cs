using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Web.Models.RentManage;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;
using Newtonsoft.Json.Linq;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class RentManageController : BaseController
    {
        // GET: RentManage

        public IBorrowAndReturnRecordInfoDataProvider IBorrowAndReturnRecordInfoDataProvider;

        public RentManageController()
        {
            this.IBorrowAndReturnRecordInfoDataProvider = new BorrowAndReturnRecordInfoDataProvider();
        }
        public ActionResult Index()
        {
            RentManageIndexModel model = new RentManageIndexModel();

            model = RentManageIndexModel.GetViewModel();
            
            return View(model);
        }

        public string RejectUserRequest(string q)
        {
            try
            {
                JObject obj = JObject.Parse(q);
                long id = long.Parse((string)obj["idStr"]);
                bool errorOrNot;
                if ((string)obj["isChecked"] == "true")
                {
                    errorOrNot = true;
                }
                else
                {
                    errorOrNot = false;
                }
                string comments = (string)obj["comments"];

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(id);
                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
                BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);
                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                BookDetailInfo bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
                
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processInfo = new ProcessRecord();
                processInfo.Status = RentRecordStatus.Rejected;
                processInfo.UserInfo = this.LoginUser();
                processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                processInfo.Comments = comments;
                iProcessRecordDataProvider.Add(processInfo);

                borrowAndReturnRecordInfo.Status = RentRecordStatus.Rejected;
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);

                BookModel bookModel = BookModel.GetViewModel(bookInfo);
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
                
                iBookDetailInfoDataProvider.Update(bookDetailInfo);
                iBookInfoDataProvider.Update(bookModel.GetEntity());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        public string ApproveUserRequest(string q)
        {
            try
            {
                JObject obj = JObject.Parse(q);
                long id = long.Parse((string)obj["idStr"]);
                string comments = (string)obj["comments"];

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(id);
               
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processInfo = new ProcessRecord();
                processInfo.Status = RentRecordStatus.Approved;
                processInfo.UserInfo = this.LoginUser();
                processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                processInfo.Comments = comments;
                iProcessRecordDataProvider.Add(processInfo);

                borrowAndReturnRecordInfo.Status = RentRecordStatus.Approved;
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        public string TakeWaitingForTake(string q)
        {
            try
            {
                long id = long.Parse(q);
                
                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(id);

                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processInfo = new ProcessRecord();
                processInfo.Status = RentRecordStatus.Taked;
                processInfo.UserInfo = this.LoginUser();
                processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                processInfo.Comments = "";
                iProcessRecordDataProvider.Add(processInfo);

                borrowAndReturnRecordInfo.Status = RentRecordStatus.Taked;
                borrowAndReturnRecordInfo.Borrow_Date = DateTime.Today;
                borrowAndReturnRecordInfo.Forcast_Date = DateTime.Today.AddDays(30);
                
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        public string RevokeWaitingForTake(string q)
        {
            try
            {
                long id = long.Parse(q);

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(id);

                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processInfo = new ProcessRecord();
                processInfo.Status = RentRecordStatus.Revoked;
                processInfo.UserInfo = this.LoginUser();
                processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                processInfo.Comments = "You didn't take your book in limited time.";
                iProcessRecordDataProvider.Add(processInfo);

                borrowAndReturnRecordInfo.Status = RentRecordStatus.Revoked;
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);

                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                BookDetailInfo bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
                bookDetailInfo.Status = BookStatus.InStore;
                iBookDetailInfoDataProvider.Update(bookDetailInfo);

                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
                BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);
                bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory + 1;
                BookModel model = BookModel.GetViewModel(bookInfo);
                iBookInfoDataProvider.Update(model.GetEntity());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        public string ReturnWaitingForReturn(string q)
        {
            try
            {
                long id = long.Parse(q);

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(id);

                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processInfo = new ProcessRecord();
                processInfo.Status = RentRecordStatus.Returned;
                processInfo.UserInfo = this.LoginUser();
                processInfo.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
                processInfo.Comments = "";
                iProcessRecordDataProvider.Add(processInfo);

                borrowAndReturnRecordInfo.Status = RentRecordStatus.Returned;
                borrowAndReturnRecordInfo.Return_Date = DateTime.Today;
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);

                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                BookDetailInfo bookDetailInfo = iBookDetailInfoDataProvider.GetBookDetailInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.ID);
                bookDetailInfo.Status = BookStatus.InStore;
                iBookDetailInfoDataProvider.Update(bookDetailInfo);

                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
                BookInfo bookInfo = iBookInfoDataProvider.GetBookInfoByID(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.ID);
                bookInfo.Avaliable_Inventory = bookInfo.Avaliable_Inventory + 1;
                BookModel model = BookModel.GetViewModel(bookInfo);
                iBookInfoDataProvider.Update(model.GetEntity());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }
    
    }
 }

