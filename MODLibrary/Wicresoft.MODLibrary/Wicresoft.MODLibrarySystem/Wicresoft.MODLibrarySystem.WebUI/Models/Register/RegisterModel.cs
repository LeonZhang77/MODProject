using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.Register
{
    public class RegisterModel : BaseViewModel
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

        public int Floor
        {
            get;
            set;
        }

        public String PM
        {
            get;
            set;
        }

        public String Team
        {
            get;
            set;
        }

        public String Chinese_Name
        {
            get;
            set;
        }

        public String Wechat
        {
            get;
            set;
        }

        public UserGrade Grade
        {
            get;
            set;
        }
        public int Late_point
        {
            get;
            set;
        }

        public String Remark
        {
            get;
            set;
        }

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
            user.Floor = this.Floor;
            user.PM = this.PM;
            user.Team = this.Team;
            user.Chinese_Name = this.Chinese_Name;
            user.Wechat = this.Wechat;
            user.Grade = this.Grade;
            user.Late_point = this.Late_point;
            user.Remark = this.Remark;
            return user;
        }
    }
}
