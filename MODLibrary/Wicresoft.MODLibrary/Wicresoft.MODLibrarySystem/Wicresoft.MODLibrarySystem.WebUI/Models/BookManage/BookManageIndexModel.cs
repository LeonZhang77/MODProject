using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.BookManage
{
    public class BookManageIndexModel : Format2IndexListModel<BookInfo>
    {
        public BookManageIndexModel()
        {
            this.BookModelList = new List<BookModel>();
        }

        public string SearchBookName
        {
            get;
            set;
        }

        public Boolean IsAvaliable
        {
            get;
            set;
        }

        public long SearchSelectedID
        {
            get;
            set;
        }

        public List<SelectListItem> SearchCategoryList
        {
            get;
            set;
        }

        public List<BookModel> BookModelList
        {
            get;
            private set;
        }
    }
}