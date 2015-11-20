using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.CategoryInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface ICategoryInfoDataProvider : IBaseDataProvider<CategoryInfo>
    {
        IEnumerable<CategoryInfo> GetCategoryList();
        IEnumerable<CategoryInfo> GetCategoryList(CategoryInfoCondition categoryCondition);

        IEnumerable<CategoryInfo> GetCategoryListByParentID(long ID);
        IEnumerable<CategoryInfo> GetParentCategoryList();
        CategoryInfo GetCategoryByID(long ID);
    }
}
