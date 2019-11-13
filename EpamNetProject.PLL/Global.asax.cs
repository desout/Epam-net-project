﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.PLL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<AutofacWebTypesModule>();

            var sqlConnectionString =
                ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            var context = new MyContext(sqlConnectionString);

            builder.Register(c => context).AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<EventRepository>().As<IRepository<Event>>().InstancePerRequest();
            builder.RegisterType<EventService>().As<IEventService>()
                .InstancePerRequest();
            builder.RegisterType<VenueService>().As<IVenueService>()
                .InstancePerRequest();
            builder.Register(c => new RoleStore<UserRole>(context))
                .As<IRoleStore<UserRole, string>>()
                .InstancePerRequest();
            builder.Register(c => new UserStore<User>(context)).As<IUserStore<User>>()
                .InstancePerRequest();
            builder.RegisterType(typeof(ApplicationUserManager)).AsSelf().InstancePerRequest();
            builder.RegisterType(typeof(ApplicationRoleManager)).AsSelf().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerRequest().OnActivated(async c => await c.Instance.SetInitialData(new UserDTO
                {
                    Email = "3809766@mail.ru",
                    UserName = "desout",
                    Password = "Desoutside1",
                    Role = "admin",
                }, new List<string> {"user", "admin"}));

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
