using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;


namespace Wicresoft.MODLibrarySystem.Web.Models.CategoryManage
{
    public class CategoryManageIndexModel : BaseIndexListModel<CategoryInfo>
    {
        public CategoryManageIndexModel()
        {
            this.CategoryModelList = new List<CategoryModel>();
        }

        public String FilterName
        {
            get;
            set;
        }

        public List<CategoryModel> CategoryModelList
        {
            get;
            private set;
        }
    }
}