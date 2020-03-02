using System.Diagnostics;
using System.Web.Mvc;

namespace EpamNetProject.PLL.Utils.filters
{
    public class ActionErrorLogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Log(filterContext.RouteData.Values["controller"] as string,
                filterContext.RouteData.Values["action"] as string,
                filterContext.Exception.Message);
        }

        private static void Log(string controllerName, string actionName, string exception)
        {
            var message = $"Action Executed: controller:{controllerName} action:{actionName} exception:{exception}";
            Debug.WriteLine(message, "Action Error Filter Log");
        }
    }
}