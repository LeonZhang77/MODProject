using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.WebServices.Models.UserModel;

namespace Wicresoft.MODLibrarySystem.WebServices.Controllers
{
    public class LoginServerController : Controller
    {
        public IUserInfoDataProvider IUserInfoDataProvider;

        public LoginServerController() 
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }
        
        public JsonResult Index(String UserName, String Password)
        {
            UserInfo user = this.IUserInfoDataProvider.FindUserInfoByUserNameAndPassword(UserName, Password);

            if (user != null)
            {
                UserLoginModel model = UserLoginModel.GetServerModel(user);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            
            else
            {
                UserLoginModel model = new UserLoginModel();
                model.statusMessage = "Didn't find this User.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        
       
    }
}