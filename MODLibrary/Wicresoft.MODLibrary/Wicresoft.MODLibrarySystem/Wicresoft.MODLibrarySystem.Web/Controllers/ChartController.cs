using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Web.Models;
using Wicresoft.MODLibrarySystem.Web.Models.Chart;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.Unity;


namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class ChartController : Controller
    {
        private ICategoryInfoDataProvider ICategoryInfoDataProvider;
        private IBookInfoDataProvider IBookInfoDataProvider;

        
        public ChartController()
        {
            this.ICategoryInfoDataProvider = new CategoryInfoDataProvider();
            this.IBookInfoDataProvider = new BookInfoDataProvider();
        }

        public ActionResult DoughnutChart()
        {
            return View();
        }
        public List<BookCountByCategory> GetListForDoughnut()
        {
            List<BookCountByCategory> countList = new List<BookCountByCategory>();
            
            List<CategoryInfo> allTopCategories = this.ICategoryInfoDataProvider.GetParentCategoryList().ToList();
            
            foreach (CategoryInfo topCategory in allTopCategories)
            {
                BookCountByCategory temp = new BookCountByCategory();
                temp.CategoryID = topCategory.ID;
                temp.CategoryName = topCategory.CategoryName;
                temp.count = 0;
                
                List<CategoryInfo> groupCategories = 
                    this.ICategoryInfoDataProvider.GetCategoryListByParentID(topCategory.ID).ToList();
                groupCategories.Add(topCategory);
                foreach(CategoryInfo categoryInfo in groupCategories)
                {
                    temp.count += categoryInfo.BookAndCategorys.Count();
                }
                countList.Add(temp);
            }

            return countList;
        }
        public JsonResult GetJSONForDoughnut()
        {
            List<BookCountByCategory> countList =  GetListForDoughnut();
            List<returnListFordoughnut> returnList = new List<returnListFordoughnut>();
            
            foreach (BookCountByCategory countItem in countList)
            {
                returnList.Add(ChartModel.GetDoughnutViewMode(countItem));
            }
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }
    }
}