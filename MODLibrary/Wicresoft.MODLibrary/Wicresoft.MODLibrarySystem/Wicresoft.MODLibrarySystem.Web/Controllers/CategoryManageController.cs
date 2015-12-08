﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.CategoryInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.Web.Models.CategoryManage;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class CategoryManageController : BaseController
    {
        public ICategoryInfoDataProvider ICategoryInfoDataProvider;

        public CategoryManageController()
        {
            this.ICategoryInfoDataProvider = new CategoryInfoDataProvider();
        }

        // GET: CategroyManage
        public ActionResult Index(String name, Int32 pageIndex = 0)
        {
            CategoryManageIndexModel model = new CategoryManageIndexModel();
            model.FilterName = name;

            CategoryInfoCondition condition = new CategoryInfoCondition();
            condition.CategoryName = name;

            IEnumerable<CategoryInfo> categorys = this.ICategoryInfoDataProvider.GetCategoryList(condition);

            PagingContent<CategoryInfo> paging = new PagingContent<CategoryInfo>(categorys, pageIndex);

            foreach (var item in paging.EntityList)
            {
                model.CategoryModelList.Add(CategoryModel.GetViewModel(item));
            }

            model.PagingContent = paging;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CategoryManageIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                name = model.FilterName
            });
        }

        public ActionResult AddCategory()
        {
            CategoryModel category = new CategoryModel();
            category.CategoryList = DropDownListHelper.GetAllCategorySelectList();
            return View(category);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel category)
        {
            CategoryInfo categoryInfo = category.GetEntity();
            CategoryInfoCondition condition = new CategoryInfoCondition();
            condition.CategoryName = categoryInfo.CategoryName;
            IEnumerable<CategoryInfo> categorys = this.ICategoryInfoDataProvider.GetCategoryList(condition);
            
            if (categorys.Count() > 0) 
            {
                if (categoryInfo.ParentCategoryInfo == null)
                {
                    categorys = categorys.Where(u => u.ParentCategoryInfo == null);
                }
                else
                {
                    categorys = categorys.Where(u => u.ParentCategoryInfo != null
                                                && u.ParentCategoryInfo.ID == categoryInfo.ParentCategoryInfo.ID);
                }

                if (categorys.Count() > 0)
                {
                    category.CategoryList = DropDownListHelper.GetAllCategorySelectList();
                    category.StateMessage = "The same category has already been exist!";
                    category.ErrorState = true;
                    return View(category);
                }
                else
                {
                    this.ICategoryInfoDataProvider.Add(categoryInfo);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                this.ICategoryInfoDataProvider.Add(categoryInfo);
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditCategory(long id)
        {
            CategoryModel category = new CategoryModel();
            CategoryInfo categoryInfo = this.ICategoryInfoDataProvider.GetCategoryByID(id);

            category = CategoryModel.GetViewModel(categoryInfo);
            if (categoryInfo.ParentCategoryInfo != null)
            {
                category.CategorySelectedID = categoryInfo.ParentCategoryInfo.ID;
                category.CategoryList = DropDownListHelper.GetCategorySelectListBySelectedID(categoryInfo.ParentCategoryInfo.ID, categoryInfo.ID);
            }
            else
            {
                category.CategoryList = DropDownListHelper.GetAllCategorySelectList(categoryInfo.ID);
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryModel category)
        {
            if (category != null && !DropDownListHelper.ValidateCategory(category.ID, category.CategorySelectedID))
            {
                this.ICategoryInfoDataProvider.Update(category.GetEntity());
                return RedirectToAction("Index");
            }
            else
            {
                category.CategoryList = DropDownListHelper.GetCategorySelectListBySelectedID(category.CategorySelectedID, category.ID);
                return View(category);
            }
        }

        public ActionResult DeleteCategory(long id)
        {
            this.ICategoryInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}