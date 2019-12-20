using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace EpamNetProject.PLL.filters
{
    public class ActionLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log(filterContext.RouteData.Values["controller"] as string,
                filterContext.RouteData.Values["action"] as string);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log(filterContext.RouteData.Values["controller"] as string,
                filterContext.RouteData.Values["action"] as string);
            base.OnActionExecuted(filterContext);
        }

        private static void Log(string controllerName, string actionName)
        {
            var message = $"Action Executed: controller:{controllerName} action:{actionName}";
            Debug.WriteLine(message, "Action Filter Log");
        }
    }
}
