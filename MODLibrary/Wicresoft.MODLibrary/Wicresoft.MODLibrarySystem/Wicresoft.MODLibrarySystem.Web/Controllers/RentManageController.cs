using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Web.Models.RentManage;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class RentManageController : Controller
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
    }
}