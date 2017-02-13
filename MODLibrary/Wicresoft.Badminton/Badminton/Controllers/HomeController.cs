using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.Home;
using System.Security.Claims;
using System.Threading;
using System.Web.Security;

namespace Badminton.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            return View(model);
        }

        public ActionResult LoginUserDisplay()
        {
            ViewBag.IsAdmin = false;
            ViewBag.IsLogin = false;

            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewBag.IsLogin = true;
                ViewBag.Email = claimsPrincipal.Identity.Name;
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                string role = ticket.UserData;
                if ("Admin".Equals(role))
                {
                    ViewBag.IsAdmin = true;
                }
                
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