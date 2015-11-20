﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.AuthorInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.Web.Models.Author;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;


namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class AuthorManageController : BaseController
    {
        public IAuthorInfoDataProvider IAuthorInfoDataProvider;

         public AuthorManageController()
        {
            this.IAuthorInfoDataProvider = new AuthorInfoDataProvider();
        }
        // GET: AuthorManage
        public ActionResult Index(String name, Int32 pageIndex = 0)
        {
            AuthorManageIndexModel model = new AuthorManageIndexModel();
            model.FilterName = name;

            AuthorInfoCondition condition = new AuthorInfoCondition();
            condition.AuthorName = name;

            IEnumerable<AuthorInfo> AuthorList = this.IAuthorInfoDataProvider.GetAuthorList(condition);

            PagingContent<AuthorInfo> paging = new PagingContent<AuthorInfo>(AuthorList,pageIndex,2);

            foreach (var item in paging.EntityList)
            {
                model.AuthorModelList.Add(AuthorModel.GetViewModel(item));
            }
            model.PagingContent = paging;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AuthorManageIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                name = model.FilterName
            });
        }

        public ActionResult AddAuthor()
        {
            AuthorModel Author = new AuthorModel();
            return View(Author);
        }

        [HttpPost]
        public ActionResult AddAuthor(AuthorModel Author)
        {
            AuthorInfo AuthorInfo = Author.GetEntity();

            this.IAuthorInfoDataProvider.Add(AuthorInfo);

            return RedirectToAction("Index");
        }


        public ActionResult EditAuthor(long id)
        {
            AuthorModel author = new AuthorModel();
            AuthorInfo authorInfo = this.IAuthorInfoDataProvider.GetAuthorListByID(id);
            if (authorInfo != null)
            {
                author = AuthorModel.GetViewModel(authorInfo);
            }

            return View(author);
        }

        [HttpPost]
        public ActionResult EditAuthor(AuthorModel Author)
        {
            if (Author != null)
            {
                this.IAuthorInfoDataProvider.Update(Author.GetEntity());
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAuthor(long id)
        {
            this.IAuthorInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}