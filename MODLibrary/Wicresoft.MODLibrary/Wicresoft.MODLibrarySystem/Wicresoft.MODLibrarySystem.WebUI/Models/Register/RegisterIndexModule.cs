using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.Register
{
    public class RegisterIndexModel
    {
        public String Email
        {
            get;
            set;
        }

        public bool IsExist
        {
            get;
            set;
        }
    }
}
