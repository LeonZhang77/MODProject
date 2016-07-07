using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Wap.Models.AccountManage
{
    public class AccountBaseModel : BaseViewModel
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

        public String Grade
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
    }
}