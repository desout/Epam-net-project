using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using EpamNetProject.PLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.PLL
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<AutofacWebTypesModule>();

            var delay = int.Parse(ConfigurationManager.AppSettings["ReserveTime"]);

            builder.RegisterType<MyContext>().WithParameter("connectionString",
                    ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString).AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<EventRepository>().As<IRepository<Event>>().InstancePerLifetimeScope();
            builder.RegisterType<EventService>().As<IEventService>()
                .InstancePerLifetimeScope()
                .WithParameter("reserveTime", delay);
            builder.RegisterType<VenueService>().As<IVenueService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoleStore<UserRole>>()
                .As<IRoleStore<UserRole, string>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>()
                .InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationUserManager)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationRoleManager)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            /*
             .OnActivated(async c => await c.Instance.SetInitialData(new UserDTO
                {
                    Email = "3809766@mail.ru",
                    UserName = "desout",
                    Password = "Desoutside1",
                    Role = "admin",
                    UserProfile = new UserProfileDTO
                        {FirstName = "Andrei", Surname = "Anelkin", Language = "en", TimeZone = "UTC-11"}
                }, new List<string> {"user", "admin"}));
             */

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var cookie = Request.Cookies["lang"];
            var cultureName = cookie != null ? cookie.Value : "en";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }
    }
}
