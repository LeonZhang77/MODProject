using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.Login
{
    public class ResetPasswordModel
    {
        public String CurrentPassword
        {
            get;
            set;
        }

        public String NewPassword 
        {
            get;
            set;
        }

        public String ConfirmPassword 
        {
            get;
            set;
        }
    }
}