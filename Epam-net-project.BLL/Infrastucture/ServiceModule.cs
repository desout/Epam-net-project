using Autofac;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Infrastucture
{
    public class ServiceModule : Module
    {
        private readonly string _connectionString;

        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventService>().As<IEventService>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<VenueService>().As<IVenueService>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<AreaRepository>().As<IAreaRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<LayoutRepository>().As<ILayoutRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<EventAreaRepository>().As<IEventAreaRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<EventSeatRepository>().As<IEventSeatRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<SeatRepository>().As<ISeatRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
            builder.RegisterType<EventRepository>().As<IEventRepository>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .InstancePerLifetimeScope();
        }
    }
}
