using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Resoure.Models.PicManager;

namespace Wicresoft.MODLibrarySystem.Resoure.Controllers
{
    public class PicManagerController : Controller
    {
        public JsonResult GetBookPicByBookID(long id)
        {
            PicModel model = new PicModel();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookPicListByBookID(long id)
        {
            PicListModel listModel = new PicListModel();
            return Json(listModel, JsonRequestBehavior.AllowGet);
        }
    }
}
