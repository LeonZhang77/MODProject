using System;
using System.Collections.Generic;
using System.Linq;
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
        public IBookDetailInfoDataProvider IBookDetailInfoDataProvider;
        public BookManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
            this.IBookDetailInfoDataProvider = new BookDetailInfoDataProvider();
        }
        // GET: BookManage
        public ActionResult Index(string bookName, long searchselectedID = 0, Int32 pageIndex = 0)
        {
            if (!string.IsNullOrEmpty(bookName))
            {
                bookName = bookName.Trim();
            }

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
            BookModel bookModel = new BookModel();
            BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
            bookModel = BookModel.GetViewModel(bookInfo);
            List<BookDetailInfo> books = this.IBookDetailInfoDataProvider.GetBookDetailList().Where(b => b.BookInfo.ID == id).ToList();
            foreach (var item in books)
            {
                bookModel.BookDetailList.Add(AddDetailBookModel.GetViewModel(item));
            }
            return View(bookModel);
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

        public ActionResult AddBooks(long id)
        {
            AddDetailBookModel addBooksModel = new AddDetailBookModel();
            BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
            addBooksModel.BookID = id;
            addBooksModel.BookName = bookInfo.BookName;
            addBooksModel.ISBN = bookInfo.ISBN;
            addBooksModel.BookStatusList = EnumHelper.GetEnumIEnumerable<BookStatus>(BookStatus.InStore);
            addBooksModel.BookStatusSelected = BookStatus.InStore;
            return View(addBooksModel);
        }

        [HttpPost]
        public ActionResult AddBooks(AddDetailBookModel books)
        {
            BookDetailInfo booksInfo = books.GetEntity();
            this.IBookDetailInfoDataProvider.Add(booksInfo);

            BookInfo book = booksInfo.BookInfo;
            book.Avaliable_Inventory = book.Avaliable_Inventory + 1;
            book.Max_Inventory = book.Max_Inventory + 1;

            this.IBookInfoDataProvider.Update(book);

            return RedirectToAction("DetailBook", new { @id = books.BookID });
        }

        public ActionResult EditBooks(long id)
        {
            AddDetailBookModel model = new AddDetailBookModel();
            BookDetailInfo booksInfo = this.IBookDetailInfoDataProvider.GetBookDetailInfoByID(id);

            if (booksInfo != null)
            {
                model = AddDetailBookModel.GetViewModel(booksInfo);
                model.BookStatusList = EnumHelper.GetEnumIEnumerable<BookStatus>(booksInfo.Status);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBooks(AddDetailBookModel model)
        {
            if (model != null)
            {
                BookDetailInfo books = model.GetEntity();
                this.IBookDetailInfoDataProvider.Update(books);

                EditBooksUpdateBook(books, model);
            }

            return RedirectToAction("DetailBook", new { @id = model.BookID });
        }

        private void EditBooksUpdateBook(BookDetailInfo books, AddDetailBookModel model)
        {
            BookInfo book = books.BookInfo;

            switch (model.BookStatusSelected)
            {
                case BookStatus.InStore:
                    if (model.BeforeStatus != BookStatus.InStore && model.BeforeStatus == BookStatus.Error)
                    {
                        book.Avaliable_Inventory = book.Avaliable_Inventory + 1;
                        book.Max_Inventory = book.Max_Inventory + 1;
                    }
                    else if (model.BeforeStatus != BookStatus.InStore)
                    {
                        book.Avaliable_Inventory = book.Avaliable_Inventory + 1;
                    }
                    break;
                case BookStatus.Rent:
                    if (model.BeforeStatus == BookStatus.InStore)
                    {
                        book.Avaliable_Inventory = book.Avaliable_Inventory - 1;
                    }
                    break;
                case BookStatus.Error:
                    if (model.BeforeStatus == BookStatus.InStore)
                    {
                        book.Avaliable_Inventory = book.Avaliable_Inventory - 1;
                        book.Max_Inventory = book.Max_Inventory - 1;
                    }
                    else if (model.BeforeStatus == BookStatus.Rent || model.BeforeStatus == BookStatus.OutStore)
                    {
                        book.Max_Inventory = book.Max_Inventory - 1;
                    }
                    break;
                case BookStatus.OutStore:
                    if (model.BeforeStatus == BookStatus.InStore)
                    {
                        book.Avaliable_Inventory = book.Avaliable_Inventory - 1;
                    }
                    break;
            }

            this.IBookInfoDataProvider.Update(book);
        }

        public ActionResult DeleteBooks(long id)
        {
            BookDetailInfo books = this.IBookDetailInfoDataProvider.GetBookDetailInfoByID(id);

            BookInfo book = books.BookInfo;
            book.Avaliable_Inventory = book.Avaliable_Inventory - 1;
            book.Max_Inventory = book.Max_Inventory - 1;

            this.IBookDetailInfoDataProvider.DeleteByID(books.ID);

            this.IBookInfoDataProvider.Update(book);

            return RedirectToAction("DetailBook", new { @id = book.ID });
        }
    }
}