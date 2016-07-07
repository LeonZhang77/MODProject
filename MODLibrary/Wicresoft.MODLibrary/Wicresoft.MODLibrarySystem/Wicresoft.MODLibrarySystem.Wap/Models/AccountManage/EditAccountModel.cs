using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Wap.Models.AccountManage
{
    public class EditAccountModel : AccountBaseModel
    {
        public bool IsSave
        {
            get;
            set;
        }

        public string SelectedFloor
        {
            get;
            set;
        }

        public List<SelectListItem> FloorList
        {
            get;
            set;
        }

        public static EditAccountModel GetViewModel(UserInfo user)
        {
            EditAccountModel model = new EditAccountModel();

            model.DisplayName = user.DisplayName;
            model.LoginName = user.LoginName;
            model.RealName = user.RealName;
            model.Email = user.Email;
            model.Floor = user.Floor;
            model.PM = user.PM;
            model.Team = user.Team;
            model.Chinese_Name = user.Chinese_Name;
            model.Wechat = user.Wechat;
            model.Grade = EnumHelper.GetEnumDescription(user.Grade);
            model.Late_point = user.Late_point;
            model.Remark = user.Remark;

            return model;
        }

        public UserInfo UpdateEntity(UserInfo user)
        {
            user.DisplayName = this.DisplayName;
            user.Email = this.Email;
            user.Floor = GetFloor(this.SelectedFloor);
            user.PM = this.PM;
            user.Team = this.Team;
            user.Chinese_Name = this.Chinese_Name;
            user.Wechat = this.Wechat;
            user.Remark = this.Remark;

            return user;
        }

        public Int32 GetFloor(string selectFloor)
        {
            Int32 floor = 0;

            if (!string.IsNullOrEmpty(selectFloor))
            {
                floor = Convert.ToInt32(selectFloor);
            }

            return floor;
        }
    }
}