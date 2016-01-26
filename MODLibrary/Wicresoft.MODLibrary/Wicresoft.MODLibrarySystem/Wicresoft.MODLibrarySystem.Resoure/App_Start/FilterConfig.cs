using System.Web;
using System.Web.Mvc;

namespace Wicresoft.MODLibrarySystem.Resoure
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}