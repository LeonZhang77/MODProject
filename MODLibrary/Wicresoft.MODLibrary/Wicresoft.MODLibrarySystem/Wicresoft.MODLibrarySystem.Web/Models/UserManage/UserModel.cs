using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.UserManage
{
    public class UserModel : BaseViewModel
    {
        public String DisplayName
        {
            get;
            set;
        }

        public String RealName
        {
            get;
            set;
        }

        public String LoginName
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public String Email
        {
            get;
            set;
        }

        public UserInfo GetEntity()
        {
            UserInfo user = new UserInfo();

            user.ID = this.ID;
            user.DisplayName = this.DisplayName;
            user.LoginName = this.LoginName;
            user.Password = this.Password;
            user.RealName = this.RealName;
            user.Email = this.Email;
            return user;
        }

        public static UserModel GetViewModel(UserInfo user)
        {
            UserModel model = new UserModel();

            model.ID = user.ID;
            model.DisplayName = user.DisplayName;
            model.RealName = user.RealName;
            model.Password = user.Password;
            model.LoginName = user.LoginName;
            model.Email = user.Email;
            return model;
        }
    }
}