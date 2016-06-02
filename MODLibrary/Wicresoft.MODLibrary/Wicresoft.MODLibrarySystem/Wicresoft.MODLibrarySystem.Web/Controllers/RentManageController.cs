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
    }
}