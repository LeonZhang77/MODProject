using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class TreeNodesHelper
    {
        private static ICategoryInfoDataProvider iCategoryInfoDataProvider;
        public static String GetCategorySelectTreeBySelectedID(long categorySelectedID, long selfID = 0)
        {
            return GetCategorySelectTree(selfID, categorySelectedID);
        }

        private static String GetCategorySelectTree(long selfID, long categorySelectedID = 0, long parentCategoryID = 0, bool isParent = false)
        {
            iCategoryInfoDataProvider = new CategoryInfoDataProvider();
            //JsonResult jsonResult = null;
            String returnStr = null;

            List<CategoryInfo> CategoryInfos = iCategoryInfoDataProvider.GetCategoryList().ToList();
            returnStr = ListToJson(CategoryInfos);

            return returnStr;
        }

        public static String ListToJson(IList<CategoryInfo> list)
        {
            String returnStr = null;
            returnStr = returnStr + "[";
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    returnStr = returnStr + "{id:" + list[i].ID.ToString() + ",";
                    if (list[i].ParentCategoryInfo == null)
                    {
                        returnStr = returnStr + "pId:0,";
                    }
                    else
                    {
                        returnStr = returnStr + "pId:" + list[i].ParentCategoryInfo.ID.ToString() + ",";
                    }
                    returnStr = returnStr + "name:\"" + list[i].CategoryName.Trim() + "\"}";
                    if (i < list.Count - 1)
                    {
                        returnStr = returnStr + ",";
                    }
                }
                
            }
            returnStr = returnStr + "]";
            return returnStr;
         }
    }
}
