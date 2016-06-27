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
        public String Username
        {
            get;
            set;
        }

        public String Grade
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
            model.Username = entity.DisplayName;
            model.Grade = EnumHelper.GetEnumDescription(entity.Grade);
            model.CreateTime = entity.CreateTime;

            return model;
        }
    }
}