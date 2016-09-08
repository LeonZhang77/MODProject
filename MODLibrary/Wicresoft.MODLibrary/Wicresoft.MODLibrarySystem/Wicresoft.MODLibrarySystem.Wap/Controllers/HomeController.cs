using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Wicresoft.MODLibrarySystem.Wap.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginUserDisplay()
        {
            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = true;
            }
            if (this.LoginUser() != null)
            {
                ViewBag.IsLogin = true;
                ViewBag.LoginName = this.LoginUser().DisplayName;
            }
            else
            {
                ViewBag.IsLogin = false;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}