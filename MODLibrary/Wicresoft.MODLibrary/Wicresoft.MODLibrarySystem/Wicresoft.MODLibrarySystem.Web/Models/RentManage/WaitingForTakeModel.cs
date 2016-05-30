using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class WaitingForTakeModel : BaseViewModel
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

        public String Email
        {
            get;
            set;
        }
        public String Floor
        {
            get;
            set;
        }
    }
}
