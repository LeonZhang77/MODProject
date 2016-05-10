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
            List<CategoryInfo> allCategories = this.ICategoryInfoDataProvider.GetCategoryList().ToList<CategoryInfo>();
            foreach (CategoryInfo categoryInfo in allCategories)
            {
                Entity.Condition.BookInfo.BookInfoCondition condition =
                    new Entity.Condition.BookInfo.BookInfoCondition();
                condition.CategoryID = categoryInfo.ID;
                int count = this.IBookInfoDataProvider.GetBookList(condition).Count();
                BookCountByCategory temp = new BookCountByCategory();
                if (categoryInfo.ParentCategoryInfo == null)
                {
                    temp.ParenetID = 0;
                }
                else
                {
                    temp.ParenetID = categoryInfo.ParentCategoryInfo.ID;
                }
                temp.CategoryID = categoryInfo.ID;
                temp.CategoryName = categoryInfo.CategoryName;
                temp.count = count;
                countList.Add(temp);
            }

            for (int i = countList.Count - 1; i >= 0; i--)
            {
                if (countList[i].ParenetID != 0)
                {
                    BookCountByCategory parenetCountItem = countList.Find(
                        delegate(BookCountByCategory countItem)
                        {
                            return countItem.CategoryID == countList[i].ParenetID;
                        });
                    parenetCountItem.count += countList[i].count;
                    countList.Remove(countList[i]);
                }
            }

            return countList;
        }
        public JsonResult GetJSONForDoughnut()
        {
            List<BookCountByCategory> countList =  GetListForDoughnut();
            List<returnListFordoughnut> returnList = new List<returnListFordoughnut>();
            
            foreach (BookCountByCategory countItem in countList)
            {
                returnListFordoughnut returnItem = new returnListFordoughnut();
                returnItem.value = countItem.count;
                returnItem.title = countItem.CategoryName;
                returnItem.color = Unity.Helper.ColorUnity.GetRandomColor();
                returnList.Add(returnItem);
            }
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }
    }
}