using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using System.Security.Claims;
using System.Threading;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public IUserInfoDataProvider IUserInfoDataProvider;

        public BaseController()
        {
            this.IUserInfoDataProvider = new UserInfoDataProvider();
        }
        public UserInfo LoginUser()
        {
            UserInfo user = null;
            ClaimsPrincipal claimsPrincipal=Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal != null&&claimsPrincipal.Identity.IsAuthenticated)
            {
                long userID = 0;
                long.TryParse(claimsPrincipal.Identity.Name, out userID);
                if (userID > 0)
                {
                    user = this.IUserInfoDataProvider.GetUserListByID(userID);
                }
                else
                {
                    user = this.IUserInfoDataProvider.GetUserListByEmail(claimsPrincipal.Identity.Name);
                }
            }
            return user;
        }
    }
}