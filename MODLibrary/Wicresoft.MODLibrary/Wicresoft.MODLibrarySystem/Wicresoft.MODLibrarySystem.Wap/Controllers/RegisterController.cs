using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.UserInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.Wap.Models.Register;

namespace Wicresoft.MODLibrarySystem.Wap.Controllers
{
    public class RegisterController : Controller
    {
        public IUserInfoDataProvider IUserInfoDataProvider;

        public RegisterController()
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }

        // GET: Register
        public ActionResult Index()
        {
            RegisterIndexModel model = new RegisterIndexModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RegisterIndexModel model)
        {
            UserInfo useInfo = model.GetEntity();
            this.IUserInfoDataProvider.Add(useInfo);
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(useInfo.ID.ToString(), true);
            return RedirectToAction("Index", "Home", null);                
        }

        [HttpPost]
        public bool validateEmail(string email)
        {
            if (this.IUserInfoDataProvider.GetUserListByEmail(email) == null)
                return false;
            else
                return true;
        }

        [HttpPost]
        public bool validateName(string name)
        {
            if (this.IUserInfoDataProvider.GetUserListByLoginName(name).Count() > 0)
                return false;
            else
                return true;
        }
    }
}