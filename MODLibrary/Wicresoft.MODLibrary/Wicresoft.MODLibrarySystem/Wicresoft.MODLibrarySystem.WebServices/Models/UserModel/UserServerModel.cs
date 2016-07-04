using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.WebServices.Models.UserModel
{
    public class UserServerModel : BaseModel
    {
        public String DisplayName
        {
            get;
            set;
        }

        public String Grade
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

        public String statusMessage
        {
            get;
            set;
        }

        public static UserServerModel GetServerModel(UserInfo entity)
        {
            UserServerModel model = new UserServerModel();

            model.ID = entity.ID;
            model.DisplayName = entity.DisplayName;
            model.Grade = EnumHelper.GetEnumDescription(entity.Grade);
            model.Email = entity.Email;
            model.Password = entity.Password;
            model.CreateTime = entity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return model;
        }

        public UserInfo UpdateEntity(UserInfo user, String Username, String Password, String Email)
        {
            user.DisplayName = Username;
            user.Password = Password;
            user.Email = Email;
            return user;
        }
    }
}