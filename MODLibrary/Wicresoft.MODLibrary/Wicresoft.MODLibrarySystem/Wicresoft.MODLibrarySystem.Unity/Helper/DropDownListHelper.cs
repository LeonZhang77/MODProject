using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class DropDownListHelper
    {
        private static ICategoryInfoDataProvider iCategoryInfoDataProvider;
        private static bool _IsSubflag;
        public static List<SelectListItem> GetAllCategorySelectList(long selfID = 0)
        {
            return GetCategorySelectList(selfID);
        }
        public static List<SelectListItem> GetParentCategorySelectList(long selfID = 0)
        {
            return GetCategorySelectList(selfID, 0, 0, true);
        }
        public static List<SelectListItem> GetParentCategorySelectListBySelectedID(long categorySelectedID, long selfID = 0)
        {
            return GetCategorySelectList(selfID, categorySelectedID, 0, true);
        }
        public static List<SelectListItem> GetCategorySelectListByParentCategoryIDAndSelectedID(long categorySelectedID, long parentCategoryID, long selfID = 0)
        {
            return GetCategorySelectList(selfID, categorySelectedID, parentCategoryID);
        }
        public static List<SelectListItem> GetCategorySelectListBySelectedID(long categorySelectedID, long selfID = 0)
        {
            return GetCategorySelectList(selfID, categorySelectedID);
        }
        private static List<SelectListItem> GetCategorySelectList(long selfID, long categorySelectedID = 0, long parentCategoryID = 0, bool isParent = false)
        {
            iCategoryInfoDataProvider = new CategoryInfoDataProvider();
            List<SelectListItem> categorySelectList = new List<SelectListItem>();
            categorySelectList.Add(new SelectListItem { Text = UntityContent.DropDwonListEmptyName, Value = "0" });
            if (isParent)
            {
                List<CategoryInfo> CategoryInfos = iCategoryInfoDataProvider.GetParentCategoryList().ToList();
                foreach (var item in CategoryInfos)
                {
                    InsertSelectListItem(categorySelectList, item, 0);
                }
            }
            else if (parentCategoryID > 0)
            {
                List<CategoryInfo> CategoryInfos = iCategoryInfoDataProvider.GetCategoryListByParentID(parentCategoryID).ToList();
                foreach (var item in CategoryInfos)
                {
                    InsertSelectListItem(categorySelectList, item, 0);
                    List<CategoryInfo> subCategoryInfos = iCategoryInfoDataProvider.GetCategoryListByParentID(item.ID).ToList();
                    if (subCategoryInfos.Count > 0)
                    {
                        AddSubCategory(categorySelectList, subCategoryInfos, 0);
                    }
                }
            }
            else
            {
                List<CategoryInfo> CategoryInfos = iCategoryInfoDataProvider.GetParentCategoryList().ToList();
                foreach (var item in CategoryInfos)
                {
                    InsertSelectListItem(categorySelectList, item, 0);
                    List<CategoryInfo> subCategoryInfos = iCategoryInfoDataProvider.GetCategoryListByParentID(item.ID).ToList();
                    if (subCategoryInfos.Count > 0)
                    {
                        AddSubCategory(categorySelectList, subCategoryInfos, 0);
                    }
                }
            }

            if (categorySelectedID > 0)
            {
                SelectListItem temp = categorySelectList.Where(c => c.Value == categorySelectedID.ToString()).FirstOrDefault();
                if (temp != null)
                {
                    temp.Selected = true;
                }
            }
            if (selfID > 0)
            {
                SelectListItem temp = categorySelectList.Where(c => c.Value == selfID.ToString()).FirstOrDefault();
                temp.Disabled = true;
            }

            return categorySelectList;
        }

        public static void AddSubCategory(List<SelectListItem> categorySelectList, List<CategoryInfo> subCategoryInfos, int currentLevel)
        {
            currentLevel = currentLevel + 1;
            foreach (var item in subCategoryInfos)
            {
                InsertSelectListItem(categorySelectList, item, currentLevel);
                List<CategoryInfo> nextSubCategoryInfos = iCategoryInfoDataProvider.GetCategoryListByParentID(item.ID).ToList();
                if (nextSubCategoryInfos.Count > 0)
                {
                    AddSubCategory(categorySelectList, nextSubCategoryInfos, currentLevel);
                }
            }
        }

        public static void InsertSelectListItem(List<SelectListItem> categorySelectList, CategoryInfo categoryInfo, int subLevel)
        {
            string breakStr = string.Empty;
            for (int i = 0; i < subLevel; i++)
            {
                breakStr += UntityContent.DropDwonListPathTemplate;
            }
            categorySelectList.Add(new SelectListItem { Text = breakStr + categoryInfo.CategoryName, Value = categoryInfo.ID.ToString() });
        }

        public static String GetParentCategoryName(CategoryInfo category)
        {
            StringBuilder strPathName = new StringBuilder();

            if (category.ParentCategoryInfo != null)
            {
                strPathName.Append(category.ParentCategoryInfo.CategoryName);
                InsertParentCategoryName(category.ParentCategoryInfo, strPathName);
            }

            return strPathName.ToString();
        }

        public static void InsertParentCategoryName(CategoryInfo pcategory, StringBuilder strPathName)
        {
            if (pcategory.ParentCategoryInfo != null)
            {
                strPathName.Append(UntityContent.CategoryPathTemplate + pcategory.ParentCategoryInfo.CategoryName);
                InsertParentCategoryName(pcategory.ParentCategoryInfo, strPathName);
            }
        }

        public static bool ValidateCategory(long selfID, long categoryID)
        {
            _IsSubflag = false;
            iCategoryInfoDataProvider = new CategoryInfoDataProvider();
            CategoryInfo category = iCategoryInfoDataProvider.GetCategoryByID(categoryID);
            if (category != null)
            {
                CategoryCheckSearch(category, selfID);
            }

            return _IsSubflag;
        }

        public static void CategoryCheckSearch(CategoryInfo category, long selfID)
        {
            if (category.ParentCategoryInfo != null)
            {
                if (category.ParentCategoryInfo.ID == selfID)
                {
                    _IsSubflag = true;
                }
                else if (category.ParentCategoryInfo.ParentCategoryInfo != null)
                {
                    CategoryCheckSearch(category.ParentCategoryInfo, selfID);
                }
            }
        }

        public static List<SelectListItem> GetFloorSelectList(string selectFloor, string path)
        {
            List<SelectListItem> floors = new List<SelectListItem>();
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            string xmlLocation = "http://" + path + "/Preference.xml";
            XmlReader reader = XmlReader.Create(xmlLocation, settings);
            
            xmlDoc.Load(reader);

            XmlNode xn = xmlDoc.SelectSingleNode("Floors");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xnInList in xnl)
            {
                string tempFloor = xnInList.InnerText;
                if (String.Equals(tempFloor,selectFloor))
                {
                    floors.Add(new SelectListItem { Text = tempFloor + "F", Value = tempFloor, Selected = true });
                }
                else
                {
                    floors.Add(new SelectListItem { Text = tempFloor + "F", Value = tempFloor });
                }
            }
            floors.Add(new SelectListItem { Text = "Please Choose", Value = "", Selected = true });

            reader.Close();
            return floors;
        }
    }
}
