using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebServices.Models.BookModel;

namespace Wicresoft.MODLibrarySystem.WebServices.Controllers
{
    public class BooksServerController : BaseController
    {
        public IBookInfoDataProvider IBookInfoDataProvider;
        public BooksServerController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
        }

        // GET: BooksServer
        public JsonResult Index(string bookName, int pageIndex = 0)
        {
            List<BookServerModel> modelList = new List<BookServerModel>();

            BookInfoCondition condition = new BookInfoCondition();
            condition.BookName = bookName;

            IEnumerable<BookInfo> books = this.IBookInfoDataProvider.GetBookList(condition);

            PagingContent<BookInfo> paging = new PagingContent<BookInfo>(books, pageIndex);

            foreach (var item in paging.EntityList)
            {
                modelList.Add(BookServerModel.GetServerModel(item));
            }

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }
    }
}