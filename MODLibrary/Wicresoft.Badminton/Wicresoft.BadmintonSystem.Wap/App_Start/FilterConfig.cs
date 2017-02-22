using System.Web;
using System.Web.Mvc;

namespace Wicresoft.BadmintonSystem.Wap
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
