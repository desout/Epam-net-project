using System.Configuration;
using Autofac;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Managers;
using EpamNetProject.PLL.Services;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Infrastucture
{
    public static class RegisterHelper
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            var delay = int.Parse(ConfigurationManager.AppSettings["ReserveTime"]);
            builder.RegisterType<MyContext>().WithParameter("connectionString",
                    ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<MapperConfigurationProvider>().As<IMapperConfigurationProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserMapperConfigurationProvider>().As<IUserMapperConfigurationProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AsyncRepository<>)).As(typeof(IAsyncRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterType<EventRepository>().As<IRepository<Event>>().InstancePerLifetimeScope();

            builder.RegisterType<EventService>().As<IEventService>()
                .InstancePerLifetimeScope()
                .WithParameter("reserveTime", delay);
            builder.RegisterType<VenueService>().As<IVenueService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ClaimService>().As<IClaimService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MyUserService>().As<IMyUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MyRoleStore>()
                .As<IRoleStore<UserRole, string>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MyUserStore>()
                .As<IUserStore<User, string>>()
                .As<IUserClaimStore<User, string>>()
                .As<IUserPasswordStore<User, string>>()
                .As<IUserRoleStore<User, string>>()
                .SingleInstance()
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(ApplicationUserManager)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationUserRoleManager)).AsSelf().InstancePerLifetimeScope();
        }
    }
}