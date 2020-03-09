using System.Web.Mvc;

namespace EpamNetProject.PLL.Areas.Manager
{
    public class ManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Manager";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Manager_default",
                "Manager/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}