using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.PLL.Infrastucture;
using EpamNetProject.PLL.Jobs;

namespace EpamNetProject.PLL
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<AutofacWebTypesModule>();
            RegisterHelper.RegisterTypes(builder);

            RegisterScheduler(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.Current.GetService<IEventService>().CheckReservationAll();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var cookie = Request.Cookies["lang"];
            var cultureName = cookie != null ? cookie.Value : "en";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        private static void RegisterScheduler(ContainerBuilder builder)
        {
            var schedulerConfig = new NameValueCollection
            {
                {"quartz.threadPool.threadCount", "1000"},
                {"quartz.scheduler.threadName", "MyScheduler"}
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });
            var delay = int.Parse(ConfigurationManager.AppSettings["basketLeaveTime"]);

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(BasketJob).Assembly));
            builder.RegisterType<BasketScheduler>().AsSelf().WithParameter("basketLeaveTime", delay);
        }
    }
}
