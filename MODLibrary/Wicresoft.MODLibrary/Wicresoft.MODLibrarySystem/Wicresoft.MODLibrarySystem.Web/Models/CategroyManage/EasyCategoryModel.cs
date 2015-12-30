using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.CategroyManage
{
    public class EasyCategoryModel : BaseCategoryModel
    {
        public bool IsChecked
        {
            get;
            set;
        }

        private static EasyCategoryModel GetEasyCategoryModel(SelectListItem categorySelectListItem)
        {
            EasyCategoryModel model = new EasyCategoryModel();

            model.CategoryName = categorySelectListItem.Text;
            model.ID = Convert.ToInt32(categorySelectListItem.Value);

            return model;
        }

        public static List<EasyCategoryModel> GetEasyCategoryModelList(List<SelectListItem> categorySelectList, ICollection<BookAndCategoryRelation> bookAndCategorys)
        {
            List<EasyCategoryModel> list = new List<EasyCategoryModel>();

            if (categorySelectList.Count() > 0)
            {
                categorySelectList.Remove(categorySelectList.FirstOrDefault(c => c.Value == "0"));

                foreach (var item in categorySelectList)
                {
                    EasyCategoryModel model = EasyCategoryModel.GetEasyCategoryModel(item);
                    list.Add(model);
                }
            }

            if (bookAndCategorys != null)
            {
                foreach (var item in bookAndCategorys)
                {
                    EasyCategoryModel tempModel = list.FirstOrDefault(m => m.ID == item.Category_ID);
                    tempModel.IsChecked = true;
                }
            }

            return list;
        }
    }
}