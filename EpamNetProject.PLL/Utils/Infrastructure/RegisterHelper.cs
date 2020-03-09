using System.Configuration;
using Autofac;
using EpamNetProject.BLL.Infrastructure;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Models;
using EpamNetProject.DAL.Repositories;
using EpamNetProject.PLL.Services;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Managers;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Utils.Infrastructure
{
    public static class RegisterHelper
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            var delay = int.Parse(ConfigurationManager.AppSettings["basketLeaveTime"]);
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
            builder.RegisterType<EventRepository>().As<IEventRepository>().InstancePerLifetimeScope();

            builder.RegisterType<EventService>().As<IEventService>()
                .InstancePerLifetimeScope()
                .WithParameter("basketLeaveTime", delay);
            builder.RegisterType<VenueService>().As<IVenueService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ClaimService>().As<IClaimService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PllUserManager>().As<IPllUserManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MyRoleStore>()
                .As<IRoleStore<UserRole, string>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MyUserStore>()
                .As<IUserStore<UserDto, string>>()
                .As<IUserClaimStore<UserDto, string>>()
                .As<IUserPasswordStore<UserDto, string>>()
                .As<IUserRoleStore<UserDto, string>>()
                .SingleInstance()
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(ApplicationUserManager)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationUserRoleManager)).AsSelf().InstancePerLifetimeScope();
        }
    }
}