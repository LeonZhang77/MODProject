using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Web.Models.CategoryManage
{
    public class CategoryModel : BaseViewModel
    {
        public String CategoryName
        {
            get;
            set;
        }

        public String ParentCategoryName
        {
            get;
            set;
        }

        public long CategorySelectedID
        {
            get;
            set;
        }

        public List<SelectListItem> CategoryList
        {
            get;
            set;
        }

        public CategoryInfo GetEntity()
        {
            CategoryInfo category = new CategoryInfo();

            category.ID = this.ID;
            category.CategoryName = this.CategoryName;

            ICategoryInfoDataProvider dataProvider = new CategoryInfoDataProvider();
            CategoryInfo parentCatagory = dataProvider.GetCategoryByID(this.CategorySelectedID);
            if (parentCatagory != null)
            {
                category.ParentCategoryInfo = parentCatagory;
            }

            return category;
        }

        public static CategoryModel GetViewModel(CategoryInfo category)
        {
            CategoryModel model = new CategoryModel();

            model.ID = category.ID;
            model.CategoryName = category.CategoryName;
            model.ParentCategoryName = DropDownListHelper.GetParentCategoryName(category);           

            return model;
        }


    }
}