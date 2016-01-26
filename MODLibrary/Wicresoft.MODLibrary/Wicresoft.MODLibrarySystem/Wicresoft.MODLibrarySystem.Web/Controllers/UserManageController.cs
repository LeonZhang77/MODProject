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
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
            }
            
            UserManageIndexModel model = new UserManageIndexModel();
            model.FilterName = name;

            UserInfoCondition condition = new UserInfoCondition();
            condition.DisplayName = name;

            IEnumerable<UserInfo> users = this.IUserInfoDataProvider.GetUserList(condition);

            PagingContent<UserInfo> paging = new PagingContent<UserInfo>(users, pageIndex);

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
            user.FloorList = GetFloorList(null);
            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            UserInfo useInfo = user.GetEntity();

            string isDup = this.ValidateDuplicate(useInfo);

            if (!string.IsNullOrEmpty(isDup))
            {
                user.ErrorState = true;
                user.StateMessage = isDup;
                return View(user);
            }
            else
            {
                this.IUserInfoDataProvider.Add(useInfo);
                return RedirectToAction("Index"); 
            }
        }
            
        public ActionResult EditUser(long id)
        {
            UserModel user = new UserModel();
            UserInfo userInfo = this.IUserInfoDataProvider.GetUserListByID(id);
            if (userInfo != null)
            {
                user = UserModel.GetViewModel(userInfo);
                user.FloorList = GetFloorList(user.Floor.ToString());
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserModel user)
        {
            UserInfo userInfo = user.GetEntity();
            UserInfo originalUser = this.IUserInfoDataProvider.GetUserListByID(userInfo.ID);
            userInfo.LoginName = originalUser.LoginName;
            userInfo.Email = originalUser.Email;

            this.IUserInfoDataProvider.Update(userInfo);
            return RedirectToAction("Index");
       }

        public ActionResult DeleteUser(long id)
        {
            this.IUserInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }

        public ActionResult DetailUser(long id)
        {
            UserModel user = new UserModel();
            UserInfo userInfo = this.IUserInfoDataProvider.GetUserListByID(id);
            user = UserModel.GetViewModel(userInfo);
            return View(user);
        }

        public List<SelectListItem> GetFloorList(string selectFloor)
        {
            List<SelectListItem> floors = new List<SelectListItem>();

            for (int i = 1; i < 5; i++)
            {
                if (i.ToString() == selectFloor)
                {
                    floors.Add(new SelectListItem { Text = i + "F", Value = i.ToString(), Selected = true });
                }
                else
                {
                    floors.Add(new SelectListItem { Text = i + "F", Value = i.ToString() });
                    
                }
            }
            floors.Add(new SelectListItem { Text = "Please Choose", Value = "", Selected = true });

            return floors;
        }

        private string ValidateDuplicate(UserInfo userInfo)
        {
            string resultStr = null;
            if (this.IUserInfoDataProvider.GetUserListByLoginName(userInfo.LoginName).Count() > 0)
            {
                resultStr = "LoginName is exsit";
            }
            else
            {
                if (this.IUserInfoDataProvider.GetUserListByEmail(userInfo.Email) != null)
                {
                    resultStr = "This Email is exsit";
                }
            }
            
            return resultStr;
        }
    }
}