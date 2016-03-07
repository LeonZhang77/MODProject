using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.WebUI.Models.AccountManage;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.Register
{
    public class RegisterModel : AccountBaseModel
    {
        public void InitRegisterModel(RegisterIndexModel index)
        {
            this.Email = index.Email;
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
