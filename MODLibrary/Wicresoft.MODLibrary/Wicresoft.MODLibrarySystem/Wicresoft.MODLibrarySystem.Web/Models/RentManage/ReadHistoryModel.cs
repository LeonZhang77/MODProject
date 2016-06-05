using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class ReadHistoryModel : BaseViewModel
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

        public String RentDateFrom
        {
            get;
            set;
        }
        public String RentDateTo
        {
            get;
            set;
        }
    }
}
