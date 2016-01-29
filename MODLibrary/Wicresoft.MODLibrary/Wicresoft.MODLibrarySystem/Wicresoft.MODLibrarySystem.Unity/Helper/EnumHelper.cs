using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class EnumHelper
    {
        public static String GetEnumDescription<TEnum>(TEnum enumerationValue)
            where TEnum : struct
        {
            Type type = enumerationValue.GetType();

            MemberInfo[] memberinfo = type.GetMember(enumerationValue.ToString());

            if (memberinfo != null && memberinfo.Length > 0)
            {
                object[] attrs = memberinfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return String.Empty;
        }


        public static IEnumerable<TEnum> GetEnumIEnumerable<TEnum>()
           where TEnum : struct
        {
            List<TEnum> enumList = new List<TEnum>();

            foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
            {
                enumList.Add(item);
            }

            return enumList;
        }

        public static List<SelectListItem> GetEnumIEnumerable<TEnum>(TEnum SelectedEnum)
            where TEnum : struct
        {
            List<SelectListItem> enumList = new List<SelectListItem>();

            foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
            {
                if (SelectedEnum.ToString() == item.ToString())
                {
                    enumList.Add(new SelectListItem { Text = GetEnumDescription<TEnum>(item), Value = Convert.ToInt32(item).ToString(), Selected = true });
                }
                else
                {
                    enumList.Add(new SelectListItem { Text = GetEnumDescription<TEnum>(item), Value = Convert.ToInt32(item).ToString(), Selected = false });
                }
            }

            return enumList;

        }
    }
}
