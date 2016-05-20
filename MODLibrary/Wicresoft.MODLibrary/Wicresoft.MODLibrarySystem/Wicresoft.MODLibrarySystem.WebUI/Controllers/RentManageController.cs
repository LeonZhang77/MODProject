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
        public RentManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
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
            //try 
            //{ 
            //    //book avaliable -1
            //    // books udpate status: pending
            //    //borrowAndReturn, new record, status: pending
            //    //Process, new record, status: pending
            //}
            //catch (Exception ex)
            //{
            //    return "false";
            //}
            //return "true";
            return "false";
        }
    }
}