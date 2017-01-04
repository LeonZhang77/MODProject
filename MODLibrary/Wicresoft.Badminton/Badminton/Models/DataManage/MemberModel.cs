using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.DataManage
{
    public class MemberModel:BaseViewModel
    {
        public String Name
        {
            get;
            set;
        }
        public bool Male
        {
            get;
            set;
        }
    }
}