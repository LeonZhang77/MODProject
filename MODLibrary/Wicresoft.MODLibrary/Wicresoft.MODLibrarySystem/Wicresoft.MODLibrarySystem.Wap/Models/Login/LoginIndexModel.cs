using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Wap.Models.Login
{
    public class LoginIndexModel
    {
        public String UserName
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public bool IsFail
        {
            get;
            set;
        }

    }
}