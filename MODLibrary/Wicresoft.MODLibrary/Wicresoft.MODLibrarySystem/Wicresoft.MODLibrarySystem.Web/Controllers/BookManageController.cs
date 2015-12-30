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
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;
using Wicresoft.MODLibrarySystem.Web.Models.CategroyManage;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class BookManageController : BaseController
    {
        public IBookInfoDataProvider IBookInfoDataProvider;
        public BookManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
        }
        // GET: BookManage
        public ActionResult Index(string bookName, long searchselectedID = 0, Int32 pageIndex = 0)
        {
            BookManageIndexModel model = new BookManageIndexModel();

            BookInfoCondition condition = new BookInfoCondition();
            condition.BookName = bookName;
            condition.CategoryID = searchselectedID;

            IEnumerable<BookInfo> books = this.IBookInfoDataProvider.GetBookList(condition);

            PagingContent<BookInfo> paging = new PagingContent<BookInfo>(books, pageIndex);

            foreach (var item in paging.EntityList)
            {
                model.BookModelList.Add(BookModel.GetViewModel(item));
            }

            model.SearchBookName = bookName;
            model.SearchCategoryList = DropDownListHelper.GetCategorySelectListBySelectedID(searchselectedID);
            model.PagingContent = paging;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(BookManageIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                bookName = model.SearchBookName,
                searchselectedID = model.SearchSelectedID
            });
        }

        public ActionResult DetailBook(long id)
        {
            return View();
        }

        public ActionResult AddBook()
        {
            BookModel bookModel = new BookModel();
            bookModel.ChooseCategoryModelList = EasyCategoryModel.GetEasyCategoryModelList(DropDownListHelper.GetAllCategorySelectList(), null);
            return View(bookModel);
        }

        [HttpPost]
        public ActionResult AddBook(BookModel book)
        {
            BookInfo bookInfo = book.GetEntity();
            this.IBookInfoDataProvider.Add(bookInfo);

            return RedirectToAction("Index");
        }

        public ActionResult EditBook(long id)
        {
            BookModel bookModel = new BookModel();
            BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
            if (bookInfo != null)
            {
                bookModel = BookModel.GetViewModel(bookInfo);
                bookModel.ChooseCategoryModelList = EasyCategoryModel.GetEasyCategoryModelList(DropDownListHelper.GetAllCategorySelectList(), bookInfo.BookAndCategorys);
            }

            return View(bookModel);
        }

        [HttpPost]
        public ActionResult EditBook(BookModel book)
        {
            if (book != null)
            {
                this.IBookInfoDataProvider.Update(book.GetEntity());
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteBook(long id)
        {
            this.IBookInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}