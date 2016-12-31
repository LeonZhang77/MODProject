using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wicresoft.MODLibrarySystem.WebUI.Models.RentManage;
using Wicresoft.MODLibrarySystem.WebUI.Models.BookManage;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Unity.Helper;


namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class RentManageController : BaseController
    {
        public IBookInfoDataProvider IBookInfoDataProvider;
        public IBookDetailInfoDataProvider IBookDetailInfoDataProvider;
        public IBorrowAndReturnRecordInfoDataProvider IBorrowAndReturnRecordInfoDataProvider;
        public IProcessRecordDataProvider IProcessRecordDataProvider;
        public IDelayRecordDataProvider IDelayRecordDataProvider;
        public RentManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
            this.IBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
            this.IBorrowAndReturnRecordInfoDataProvider = new BorrowAndReturnRecordInfoDataProvider();
            this.IProcessRecordDataProvider = new ProcessRecordDataProvider();
            this.IDelayRecordDataProvider = new DelayRecordDataProvider();
        }

        public ActionResult Index()
        {
            RentManageIndexModel model = new RentManageIndexModel();
            model = RentManageIndexModel.GetViewModel(this.LoginUser());

            return View(model);
        }
        public ActionResult MyRequest()
        {
            UserInfo userInfo = this.LoginUser();
            MyRequestModel model = new MyRequestModel();
            List<MyRequestModel> tempList = MyRequestModel.GetMoreRequestList(userInfo);

            model.MyRequestList = tempList;

            return View(model);
        }
        public ActionResult ReadHistory()
        {
            ReadHistoryModel model = new ReadHistoryModel();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Returned, this.LoginUser()).OrderByDescending(b => b.Return_Date).ToList();

            int count = tempList.Count();
            for (int i = 0; i < count; i++)
            {
                model.ReadHistoryModelList.Add(ReadHistoryModel.GetViewModel(tempList[i]));
            }

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
                    BookToRentModel model = new BookToRentModel();
                    BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                    BookModel bookModel = new BookModel();
                    ProcessRecord processRecord = model.GetEntity(id, this.LoginUser(), out borrowAndReturnRecordInfo, out bookDetailInfo, out bookModel);

                    this.IBorrowAndReturnRecordInfoDataProvider.Add(borrowAndReturnRecordInfo);
                    this.IProcessRecordDataProvider.Add(processRecord);
                    this.IBookDetailInfoDataProvider.Update(bookDetailInfo);
                    this.IBookInfoDataProvider.Update(bookModel.GetEntity());

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        public string RenewBookInHand(string q)
        {
            try
            {
                int id = Convert.ToInt32(q);
                BookInHandModel model = new BookInHandModel();
                model.ID = id;

                BorrowAndReturnRecordInfo borrowAndReturnRecordInfo = new BorrowAndReturnRecordInfo();
                DelayRecord delayRecord = model.GetEntity(this.LoginUser(), out borrowAndReturnRecordInfo);

                this.IBorrowAndReturnRecordInfoDataProvider.Update(borrowAndReturnRecordInfo);
                this.IDelayRecordDataProvider.Add(delayRecord);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }
    }
}