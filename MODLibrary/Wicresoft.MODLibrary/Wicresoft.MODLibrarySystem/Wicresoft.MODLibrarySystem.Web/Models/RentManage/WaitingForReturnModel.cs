using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class WaitingForReturnModel : BaseViewModel
    {
        public String Title
        {
            get;
            set;
        }
        public String UserName
        {
            get;
            set;
        }

        public DateTime ExpirationDay
        {
            get;
            set;
        }
    }
}
