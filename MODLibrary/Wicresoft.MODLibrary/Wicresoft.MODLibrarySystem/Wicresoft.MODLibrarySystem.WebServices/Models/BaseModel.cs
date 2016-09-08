using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.WebServices.Models
{
    public class BaseModel
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
    }
}