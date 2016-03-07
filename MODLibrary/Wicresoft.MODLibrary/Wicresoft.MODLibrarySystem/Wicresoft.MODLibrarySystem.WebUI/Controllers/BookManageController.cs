﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebUI.Models.BookManage;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class BookManageController : Controller
    {
        public IBookInfoDataProvider IBookInfoDataProvider;

        public BookManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
        }
        // GET: BookManage
        public ActionResult Index(string bookName, long searchselectedID = 0, bool? isAvaliable = null, Int32 pageIndex = 0)
        {
            if (!string.IsNullOrEmpty(bookName))
            {
                bookName = bookName.Trim();
            }

            BookManageIndexModel model = new BookManageIndexModel();

            BookInfoCondition condition = new BookInfoCondition();
            condition.BookName = bookName;
            condition.CategoryID = searchselectedID;
            condition.IsAvaliable = isAvaliable;

            IEnumerable<BookInfo> books = this.IBookInfoDataProvider.GetBookList(condition);

            PagingContent<BookInfo> paging = new PagingContent<BookInfo>(books, pageIndex, 6);

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
                searchselectedID = model.SearchSelectedID,
                isAvaliable = model.IsAvaliable
            });
        }
    }
}