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
           
        public UserInfo GetEntity()
        {
            UserInfo user = new UserInfo();

            user.ID = this.ID;
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

        public static UserModel GetViewModel(UserInfo user)
        {
            UserModel model = new UserModel();

            model.ID = user.ID;
            model.DisplayName = user.DisplayName;
            model.RealName = user.RealName;
            model.Password = user.Password;
            model.LoginName = user.LoginName;
            model.Email = user.Email;
            model.Floor = user.Floor;
            model.PM = user.PM;
            model.Team = user.Team;
            model.Chinese_Name = user.Chinese_Name;
            model.Wechat = user.Wechat;
            model.Grade = user.Grade;
            model.Late_point = user.Late_point;
            user.Remark = user.Remark;
            return model;
        }
    }
}