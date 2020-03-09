using System.Web.Mvc;
using EpamNetProject.PLL.Utils.filters;

namespace EpamNetProject.PLL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionLogAttribute());
        }
    }
}