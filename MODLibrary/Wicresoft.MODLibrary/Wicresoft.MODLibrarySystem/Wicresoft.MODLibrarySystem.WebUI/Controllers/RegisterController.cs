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
using Wicresoft.MODLibrarySystem.WebUI.Models.Register;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
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
            if (model.Email == null)
            {
                 return View(model);
            }
          
            UserInfo user = this.IUserInfoDataProvider.GetUserListByEmail(model.Email);

            if (user == null)
            {
                return RedirectToAction("RegisterUser", "Register", model);
            }
            else
            {
                model.IsExist = true;
                return View(model);
            }
        }

        public ActionResult RegisterUser(RegisterIndexModel model)
        {
            RegisterModel userModel = new RegisterModel();
            userModel.InitRegisterModel(model);

            if (model.Email == null)
            {
                return RedirectToAction("Index", "Register", model);
            }
            else
            {
                return View(userModel);
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel userModel)
        {
            UserInfo useInfo = userModel.GetEntity();
            if (this.IUserInfoDataProvider.GetUserListByLoginName(userModel.LoginName).Count() > 0)
            {
                userModel.ErrorState = true;
                userModel.StateMessage = "LoginName is exsit";
                return View(userModel);
            }
            else
            {
                this.IUserInfoDataProvider.Add(useInfo);

                FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(useInfo.ID.ToString(), true);
                return RedirectToAction("Index", "Home", null);
            }
        }
        
    }
}