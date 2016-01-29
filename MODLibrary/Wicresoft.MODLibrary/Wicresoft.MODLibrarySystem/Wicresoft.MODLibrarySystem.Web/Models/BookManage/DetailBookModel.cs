using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Web.Models.BookManage
{
    public class DetailBookModel : BaseViewModel
    {
        public String BookName
        {
            get;
            set;
        }
        public String ISBN
        {
            get;
            set;
        }

        public String PublisherName
        {
            get;
            set;
        }
        public String AuthorName
        {
            get;
            set;
        }
        public String CatagoryName
        {
            get;
            set;
        }
        public String Publish_Date
        {
            get;
            set;
        }
        public String Price
        {
            get;
            set;
        }




    }
}