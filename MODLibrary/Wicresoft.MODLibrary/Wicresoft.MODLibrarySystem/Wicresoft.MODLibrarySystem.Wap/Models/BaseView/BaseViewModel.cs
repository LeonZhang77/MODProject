using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Wap.Models
{
    public class BaseViewModel
    {
        public long ID
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string StateMessage
        {
            get;
            set;
        }

        public bool ErrorState
        {
            get;
            set;
        }
    }
}