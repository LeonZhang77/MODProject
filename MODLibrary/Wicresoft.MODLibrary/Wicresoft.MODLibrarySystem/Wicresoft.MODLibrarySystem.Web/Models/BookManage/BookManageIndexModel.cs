using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.BookManage
{
    public class BookManageIndexModel : BaseIndexListModel<BookInfo>
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