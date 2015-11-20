using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.UserInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.Web.Models.UserManage;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class UserManageController : BaseController
    {
        // GET: UserManage
        public ActionResult Index(String name, Int32 pageIndex = 0)
        {
            UserManageIndexModel model = new UserManageIndexModel();
            model.FilterName = name;

            UserInfoCondition condition = new UserInfoCondition();
            condition.DisplayName = name;

            IEnumerable<UserInfo> users = this.IUserInfoDataProvider.GetUserList(condition);

            PagingContent<UserInfo> paging = new PagingContent<UserInfo>(users, pageIndex, 2);

            foreach (var item in paging.EntityList)
            {
                model.userModelList.Add(UserModel.GetViewModel(item));
            }

            model.PagingContent = paging;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UserManageIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                name = model.FilterName
            });
        }

        public ActionResult AddUser()
        {
            UserModel user = new UserModel();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            UserInfo useInfo = user.GetEntity();

            this.IUserInfoDataProvider.Add(useInfo);

            return RedirectToAction("Index");
        }

        public ActionResult EditUser(long id)
        {
            UserModel user = new UserModel();
            UserInfo userInfo = this.IUserInfoDataProvider.GetUserListByID(id);
            if (userInfo != null)
            {
                user = UserModel.GetViewModel(userInfo);
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserModel user)
        {
            if (user != null)
            {
                this.IUserInfoDataProvider.Update(user.GetEntity());
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUser(long id)
        {
            this.IUserInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}