using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebServices.Models.UserModel;

namespace Wicresoft.MODLibrarySystem.WebServices.Controllers
{
    public class AccountManageController : Controller
    {
        
        public IUserInfoDataProvider IUserInfoDataProvider;

        public AccountManageController() 
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }
        
        public JsonResult EditAccount(String UserName, String Password, String ID, String Email)
        {
            UserInfo user = this.IUserInfoDataProvider.GetUserListByID(Int32.Parse(ID));

            if (user != null)
            {
                UserServerModel model = UserServerModel.GetServerModel(user);
                user = model.UpdateEntity(user, UserName, Password, Email);
                this.IUserInfoDataProvider.Update(user);
                model = UserServerModel.GetServerModel(user);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                UserLoginModel model = new UserLoginModel();
                model.statusMessage = "Didn't find this User.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddAccount(String DisplayName, String RealName, String LoginName, String Password, String Email, String Wechat)
        {
            UserServerModel userModel = new UserServerModel();
            UserInfo userInfo = userModel.GetEntity(DisplayName, RealName, LoginName, Password, Email, Wechat);

            if (this.IUserInfoDataProvider.GetUserListByLoginName(LoginName).Count() > 0)
            {
                userModel.statusMessage = "LoginName is exsit";                
            }
            else
            {
                this.IUserInfoDataProvider.Add(userInfo);
                userInfo = this.IUserInfoDataProvider.GetUserListByLoginName(LoginName).ToList()[0];
                userModel = UserServerModel.GetServerModel(userInfo);
            }

            return Json(userModel, JsonRequestBehavior.AllowGet);
        }
    }
}