using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.WebUI.Models.Login;
using Wicresoft.MODLibrarySystem.WebUI.Models.LoginAndRegister;
using System.Web.Security;
using System.Security.Claims;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IUserInfoDataProvider IUserInfoDataProvider;

        public LoginController()
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }

        // GET: Login
        public ActionResult Index()
        {
            LoginIndexModel model = new LoginIndexModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginIndexModel model)
        {
            UserInfo user = this.IUserInfoDataProvider.FindUserInfoByUserNameAndPassword(model.UserName, model.Password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                model.IsFail = true;
                return View(model);
            }
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "LoginAndRegister",null);
        }

        public ActionResult NotPermission()
        {

            return View();
        }


        public ActionResult TurnToRegister()
        {
            return RedirectToAction("Index", "Register", null);
        }
    }
}