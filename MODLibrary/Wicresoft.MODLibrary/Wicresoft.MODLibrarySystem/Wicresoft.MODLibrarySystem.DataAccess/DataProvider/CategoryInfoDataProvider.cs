using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.CategoryInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class CategoryInfoDataProvider : ICategoryInfoDataProvider
    {
        private DBSource DataSource;

        public CategoryInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<CategoryInfo> GetCategoryList()
        {
            return this.DataSource.CategoryInfos;
        }

        public IEnumerable<CategoryInfo> GetCategoryList(CategoryInfoCondition categoryCondition)
        {
            var categoryList = from category in this.DataSource.CategoryInfos
                               select category;

            if (!String.IsNullOrEmpty(categoryCondition.CategoryName))
            {
                categoryList = categoryList.Where(u => u.CategoryName.Contains(categoryCondition.CategoryName));
            }

            return categoryList.ToList();
        }

        public IEnumerable<CategoryInfo> GetCategoryListByParentID(long ID)
        {
            return this.DataSource.CategoryInfos.Where(c => c.ParentCategoryInfo != null && c.ParentCategoryInfo.ID == ID);
        }

        public IEnumerable<CategoryInfo> GetParentCategoryList()
        {
            return this.DataSource.CategoryInfos.Where(c => c.ParentCategoryInfo == null);
        }

        public CategoryInfo GetCategoryByID(long ID)
        {
            CategoryInfo category = this.DataSource.CategoryInfos.FirstOrDefault(u => u.ID == ID);
            return category;
        }

        public void Add(CategoryInfo categoryInfo)
        {
            if (categoryInfo.ParentCategoryInfo != null)
            {
                categoryInfo.ParentCategoryInfo = this.DataSource.CategoryInfos.Find(categoryInfo.ParentCategoryInfo.ID);
            }
            this.DataSource.CategoryInfos.Add(categoryInfo);
            this.DataSource.SaveChanges();
        }

        public void Update(CategoryInfo categoryInfo)
        {
            CategoryInfo category = this.GetCategoryByID(categoryInfo.ID);

            category.CategoryName = categoryInfo.CategoryName;
            if (categoryInfo.ParentCategoryInfo != null)
            {
                category.ParentCategoryInfo = this.DataSource.CategoryInfos.Find(categoryInfo.ParentCategoryInfo.ID);
            }
            else
            {
                category.ParentCategoryInfo = null;
            }

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            CategoryInfo category = this.GetCategoryByID(id);

            if (category != null)
            {
                this.DataSource.CategoryInfos.Remove(category);
                this.DataSource.SaveChanges();
            }
        }
    }
}
