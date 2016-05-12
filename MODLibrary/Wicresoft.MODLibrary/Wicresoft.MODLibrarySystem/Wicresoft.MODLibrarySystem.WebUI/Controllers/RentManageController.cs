using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wicresoft.MODLibrarySystem.WebUI.Models.RentManage;


namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class RentManageController : Controller
    {
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
    }
}