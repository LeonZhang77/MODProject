using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.Login
{
    public class LoginIndexModel
    {
        public String Account
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