using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.UserManage
{
    public class UserManageIndexModel : BaseIndexListModel<UserInfo>
    {
        public UserManageIndexModel()
        {
            this.userModelList = new List<UserModel>();
        }

        public String FilterName
        {
            get;
            set;
        }

        public List<UserModel> userModelList
        {
            get;
            private set;
        }
    }
}