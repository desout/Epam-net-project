using System.Web.Mvc;

namespace EpamNetProject.PLL.Areas.Events
{
    public class EventsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Events";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Events_default",
                "Events/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}