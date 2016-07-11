using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.WebUI.Models.Register;
using Wicresoft.MODLibrarySystem.WebUI.Models.Login;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.LoginAndRegister
{
    public class LoginAndRegisterModel
    {
        public LoginIndexModel LoginIndexModel
        {
            get;
            set;
        }
        public RegisterModel RegisterModel
        {
            get;
            set;
        }
        public RegisterIndexModel RegisterIndexModel
        {
            get;
            set;
        }
    }
}