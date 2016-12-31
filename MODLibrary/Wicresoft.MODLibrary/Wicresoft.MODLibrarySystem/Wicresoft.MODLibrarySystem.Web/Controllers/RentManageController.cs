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

        public ActionResult UserRequestMore(Int32 pageIndex = 0)
        {
            UserRequestIndexModel model = UserRequestIndexModel.GetUserRequestList(pageIndex);

            return View(model);
        }

        public ActionResult WaitingForTakeMore(Int32 pageIndex = 0)
        {
            WaitingForTakeIndexModel model = WaitingForTakeIndexModel.GetWaitingForTakeList(pageIndex);

            return View(model);
        }

        public ActionResult WaitingForReturnMore(Int32 pageIndex = 0)
        {
            WaitingForReturnIndexModel model = WaitingForReturnIndexModel.GetWaitingForReturnList(pageIndex);

            return View(model);
        }

        public ActionResult RentHistoryMore(Int32 pageIndex = 0)
        {
            ReadHistoryIndexModel model = ReadHistoryIndexModel.GetReadHistoryModelList(pageIndex);

            return View(model);
        }

        public string RejectUserRequest(string q)
        {
            try
            {
                JObject obj = JObject.Parse(q);
                long id = long.Parse((string)obj["idStr"]);

                UserRequestModel model = new UserRequestModel();
                model.ID = id;

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                BookModel bookModel = new BookModel();
                BookDetailInfo bookDetailInfo = new BookDetailInfo();
                ProcessRecord processInfo = model.GetEntity(this.LoginUser(), q, true, out borrowAndReturnRecordInfo, out bookDetailInfo, out bookModel);

                iProcessRecordDataProvider.Add(processInfo);
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);
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

                UserRequestModel model = new UserRequestModel();
                model.ID = id;

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                BookModel bookModel = new BookModel();
                BookDetailInfo bookDetailInfo = new BookDetailInfo();
                ProcessRecord processInfo = model.GetEntity(this.LoginUser(), q, false, out borrowAndReturnRecordInfo, out bookDetailInfo, out bookModel);

                iProcessRecordDataProvider.Add(processInfo);
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
                WaitingForTakeModel model = new WaitingForTakeModel();
                model.ID = id;

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                ProcessRecord processInfo = model.GetTakeEntity(out borrowAndReturnRecordInfo, this.LoginUser());

                iProcessRecordDataProvider.Add(processInfo);
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
                WaitingForTakeModel model = new WaitingForTakeModel();
                model.ID = id;
                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                BookDetailInfo bookDetailInfo = new BookDetailInfo();
                BookModel bookModel = new BookModel();
                ProcessRecord processInfo = model.GetRevokeEntity(out borrowAndReturnRecordInfo, out bookDetailInfo, out bookModel, this.LoginUser());

                iProcessRecordDataProvider.Add(processInfo);
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);
                iBookDetailInfoDataProvider.Update(bookDetailInfo);
                iBookInfoDataProvider.Update(bookModel.GetEntity());
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
                WaitingForReturnModel model = new WaitingForReturnModel();
                model.ID = id;

                IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                IBookDetailInfoDataProvider iBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
                IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                BookDetailInfo bookDetailInfo = new BookDetailInfo();
                BookModel bookModel = new BookModel();

                ProcessRecord processInfo = model.GetEntity(this.LoginUser(), out borrowAndReturnRecordInfo, out bookDetailInfo, out bookModel);

                iProcessRecordDataProvider.Add(processInfo);
                iBorrowAndReturnRecordInfoDataProviderdataProvider.Update(borrowAndReturnRecordInfo);
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

