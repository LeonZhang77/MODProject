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
using Wicresoft.MODLibrarySystem.WebUI.Models.BookManage;

namespace Wicresoft.MODLibrarySystem.WebUI.Controllers
{
    public class BookManageController : BaseController
    {
        public IBookInfoDataProvider IBookInfoDataProvider;
        public ISupportORAgainstInfoDataProvider ISupportORAgainstInfoDataProvider;
        public BookManageController()
        {
            this.IBookInfoDataProvider = new BookInfoDataProvider();
            this.ISupportORAgainstInfoDataProvider = new SupportORAgainstInfoDataProvider();
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
                model.BookModelList.Add(BookModel.GetViewModel(item, this.LoginUser()));
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

        public bool SupportORAgainstBook(string q, bool flag)
        {
            try
            {
                SupportORAgainst readyToAdd = new SupportORAgainst();
                if (flag)
                {
                    readyToAdd.Status = SupportAgainstStatus.Support;
                }
                else
                {
                    readyToAdd.Status = SupportAgainstStatus.Against;
                }
                readyToAdd.UserInfo = this.LoginUser();
                readyToAdd.BookInfo = this.IBookInfoDataProvider.GetBookInfoByID(Convert.ToInt32(q));
                this.ISupportORAgainstInfoDataProvider.Add(readyToAdd);
            }
            catch(Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public ActionResult DetailTheBook(long id)
        {
            //DetailBookModel detailBookModel = new DetailBookModel();
            BookInfo bookInfo = this.IBookInfoDataProvider.GetBookInfoByID(id);
            DetailBookModel detailBookModel = DetailBookModel.GetViewModel(bookInfo, this.LoginUser());
            return View(detailBookModel);
        }
    }
}