using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Wap.Models.Register
{
    public class RegisterIndexModel
    {
        public String Email
        {
            get;
            set;
        }
        public String DisplayName
        {
            get;
            set;
        }

        public String LoginName
        {
            get;
            set;
        }

        public String RealName
        {
            get;
            set;
        }
        public String Password
        {
            get;
            set;
        }
        public String WeChat
        {
            get;
            set;
        }
        public UserInfo GetEntity()
        {
            UserInfo user = new UserInfo();

            user.DisplayName = this.DisplayName;
            user.LoginName = this.LoginName;
            user.Password = this.Password;
            user.RealName = this.RealName;
            user.Email = this.Email;
            user.Grade = UserGrade.Junior;

            return user;
        }
    }
}
