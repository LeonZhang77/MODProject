using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.WebUI.Models.LoginAndRegister;
using Wicresoft.MODLibrarySystem.WebUI.Models.Login;
using Wicresoft.MODLibrarySystem.WebUI.Models.Register;
using System.Web.Security;
using System.Security.Claims;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        public IUserInfoDataProvider IUserInfoDataProvider;

        public LoginAndRegisterController()
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }

        // GET: LoginAndRegister
        public ActionResult Index()
        {
            LoginAndRegisterModel model = new LoginAndRegisterModel();
            model.LoginIndexModel = new LoginIndexModel();
            model.RegisterIndexModel = new RegisterIndexModel();
            model.RegisterModel = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginAndRegisterModel model)
        {
            UserInfo user = this.IUserInfoDataProvider.FindUserInfoByUserNameAndPassword(model.LoginIndexModel.UserName, model.LoginIndexModel.Password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                model.LoginIndexModel.IsFail = true;
                model.RegisterIndexModel = new RegisterIndexModel();
                model.RegisterModel = new RegisterModel();
                return View(model);
            }
        }

        public ActionResult NotPermission()
        {
            return View();
        }

        public ActionResult TurnToRegister(LoginAndRegisterModel model)
        {
            return RedirectToAction("Register", "LoginAndRegister", model);
        }

        public ActionResult Register()
        {
            LoginAndRegisterModel model = new LoginAndRegisterModel();
            model.LoginIndexModel = new LoginIndexModel();
            model.RegisterIndexModel = new RegisterIndexModel();
            model.RegisterModel = new RegisterModel();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Register(LoginAndRegisterModel model)
        {
            if (model.RegisterIndexModel.Email == null)
            {
                return RedirectToAction("Register", "LoginAndRegister", model);
            }

            UserInfo user = this.IUserInfoDataProvider.GetUserListByEmail(model.RegisterIndexModel.Email);

            if (user == null)
            {
                return RedirectToAction("RegisterUser", "LoginAndRegister", model.RegisterIndexModel);
            }
            else
            {
                model.RegisterIndexModel.IsExist = true;
                model.LoginIndexModel = new LoginIndexModel();
                model.RegisterModel = new RegisterModel();
                return View(model);
            }
        }

        public ActionResult RegisterUser(RegisterIndexModel indexModel)
        {
            RegisterModel userModel = new RegisterModel();
            userModel.InitRegisterModel(indexModel);

            LoginAndRegisterModel model = new LoginAndRegisterModel();
            model.LoginIndexModel = new LoginIndexModel();
            model.RegisterIndexModel = indexModel;
            model.RegisterModel = userModel;

            if (indexModel.Email == null)
            {
                return RedirectToAction("Register", "LoginAndRegister", model);
            }
            else
            {
                
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(LoginAndRegisterModel model)
        {
            RegisterModel userModel = model.RegisterModel;
            UserInfo useInfo = userModel.GetEntity();
            if (this.IUserInfoDataProvider.GetUserListByLoginName(userModel.LoginName).Count() > 0)
            {
                userModel.ErrorState = true;
                userModel.StateMessage = "LoginName is exsit";
                model.RegisterModel = userModel;
                model.LoginIndexModel = new LoginIndexModel();
                model.RegisterIndexModel = new RegisterIndexModel();
                return View(model);
            }
            else
            {
                this.IUserInfoDataProvider.Add(useInfo);
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "LoginAndRegister");
            }
        }
    }
}