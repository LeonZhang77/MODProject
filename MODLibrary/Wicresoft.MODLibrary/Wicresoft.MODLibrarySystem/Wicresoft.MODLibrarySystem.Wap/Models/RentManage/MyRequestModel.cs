using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Wap.Models.RentManage
{
    public class MyRequestModel : BaseViewModel
    {
        public string Title
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
        public String Publish
        {
            get;
            set;
        }
        public String Status
        {
            get;
            set;
        }
        public String Comment
        {
            get;
            set;
        }
    }
}
