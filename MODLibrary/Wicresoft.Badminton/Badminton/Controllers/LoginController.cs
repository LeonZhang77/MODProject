using Badminton.Models.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Controllers
{
    public class LoginController : Controller
    {

        public LoginController()
        {

        }

        // GET: Login
        public ActionResult Index()
        {
            LoginIndexModel model = new LoginIndexModel();
            return View(model);
        }
        // Post
        [HttpPost]
        public ActionResult Index(LoginIndexModel model)
        {
            string Role = "User";
            bool IsAdmin = VerifyWebConfigByUserNameAndPassword(model.Account, model.Password);

            if (IsAdmin)
            {
                Role = "Admin";
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, model.Account, DateTime.Now, DateTime.Now.AddMinutes(20), true, Role
                    );
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.HttpOnly = true;
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home", null);
        }

        public bool VerifyWebConfigByUserNameAndPassword(String userName, String password)
        {
            String Account = null;
            String Password = null;
            if (ConfigurationManager.AppSettings.HasKeys())
            {
                Account = ConfigurationManager.AppSettings.GetValues("Account").First();
                Password = ConfigurationManager.AppSettings.GetValues("Password").First();
                if (Account.Equals(userName) && Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
        //GET: Modify Password
        public ActionResult ResetPassword()
        {
            ResetPasswordModel model = new ResetPasswordModel();
            return View(model);
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult NotPermission()
        {
            return View();
        }

    }
}