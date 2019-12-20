using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace EpamNetProject.PLL.filters
{
    public class ActionErrorLogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private static void Log(string controllerName, string actionName, string exception)
        {
            var message = $"Action Executed: controller:{controllerName} action:{actionName} exception:{exception}";
            Debug.WriteLine(message, "Action Error Filter Log");
        }

        public void OnException(ExceptionContext filterContext)
        {
            Log(filterContext.RouteData.Values["controller"] as string,
                filterContext.RouteData.Values["action"] as string,
                filterContext.Exception.Message);
        }
    }
}
