using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Web.Models.Home;
using System.Web.Security;
using System.Security.Claims;
using System.Threading;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();

            //IEnumerable<UserInfo> userInfos = this.IUserInfoDataProvider.GetUserList(true);

            //StringBuilder sbUserName = new StringBuilder();
            //foreach (var item in userInfos)
            //{
            //    sbUserName.Append(item.DisplayName + "|");
            //}

            //UserInfo userInfo = this.IUserInfoDataProvider.GetUserListByID(2);

            //if (userInfo != null)
            //{
            //    sbUserName.Append(userInfo.DisplayName);
            //}

            //model.ShowUserName = sbUserName.ToString();

            return View(model);
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

        public ActionResult TestView2()
        {
            return View();
        }
    }
}