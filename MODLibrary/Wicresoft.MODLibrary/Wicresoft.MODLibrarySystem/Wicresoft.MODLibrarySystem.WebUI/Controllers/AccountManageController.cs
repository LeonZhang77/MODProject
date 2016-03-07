using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebUI.Models.AccountManage;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class AccountManageController : BaseController
    {
        // GET: AccountManage
        public ActionResult Index()
        {
            ViewBag.UserName = this.LoginUser().DisplayName;
            return View();
        }

        public ActionResult EditAccount()
        {
            UserInfo user = this.LoginUser();
            EditAccountModel model = new EditAccountModel();

            if (user != null)
            {
                model = EditAccountModel.GetViewModel(user);
                model.FloorList = DropDownListHelper.GetFloorList(model.Floor.ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAccount(EditAccountModel model)
        {
            if (model != null)
            {
                UserInfo user = model.UpdateEntity(this.LoginUser());
                this.IUserInfoDataProvider.Update(user);
                model.FloorList = DropDownListHelper.GetFloorList(model.Floor.ToString());
                model.IsSave = true;
            }


            return View(model);
        }
    }
}