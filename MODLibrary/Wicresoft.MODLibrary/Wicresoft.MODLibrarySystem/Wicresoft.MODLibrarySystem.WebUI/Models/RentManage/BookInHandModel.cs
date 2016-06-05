using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.RentManage
{
    public class BookInHandModel : BaseViewModel
    {
        public BookInHandModel()
        {

        }
        public string Title
        {
            get;
            set;
        }
        public string StartBorrowDay
        {
            get;
            set;
        }
        public String EXpirationDay
        {
            get;
            set;
        }

        public bool EnableRenew
        {
            get;
            set;
        }

        public bool HasAlreadyRenewed
        {
            get;
            set;
        }

    }
}
