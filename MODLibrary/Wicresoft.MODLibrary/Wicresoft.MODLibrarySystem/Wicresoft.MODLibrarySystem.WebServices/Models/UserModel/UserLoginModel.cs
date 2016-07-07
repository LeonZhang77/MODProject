using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.WebServices.Models.UserModel
{
    public class UserLoginModel : BaseModel
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
        
        public String Email
        {
            get;
            set;
        }

        public String Wechat
        {
            get;
            set;
        }
        public String Grade
        {
            get;
            set;
        }

        public String Late_point
        {
            get;
            set;
        }

        public String Remark
        {
            get;
            set;
        }

        public String statusMessage
        {
            get;
            set;
        }

        public static UserLoginModel GetServerModel(UserInfo entity)
        {
            UserLoginModel model = new UserLoginModel();

            model.ID = entity.ID;
            model.DisplayName = entity.DisplayName;
            model.RealName = entity.RealName;
            model.LoginName = entity.LoginName;
            model.Password = entity.Password;
            model.Floor = entity.Floor;
            model.Team = entity.Team;
            model.Chinese_Name = entity.Chinese_Name;
            model.Email = entity.Email;
            model.Wechat = entity.Wechat;
            model.Late_point = entity.Late_point.ToString();
            model.Remark = entity.Remark;
            model.Grade = EnumHelper.GetEnumDescription(entity.Grade);

            model.CreateTime = entity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return model;
        }        
    }
}