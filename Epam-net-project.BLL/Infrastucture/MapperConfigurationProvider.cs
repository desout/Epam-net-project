using AutoMapper;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.models;

namespace EpamNetProject.BLL.Infrastucture
{
    internal static class MapperConfigurationProvider
    {
        public static IMapper GetMapperConfig()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<Area, AreaDto>();
                cfg.CreateMap<Seat, SeatDto>();
                cfg.CreateMap<Layout, LayoutDto>();
                cfg.CreateMap<Venue, VenueDto>();
                cfg.CreateMap<EventArea, EventAreaDto>();
                cfg.CreateMap<EventSeat, EventSeatDto>();
                cfg.CreateMap<EventDto, Event>();
                cfg.CreateMap<AreaDto, Area>();
                cfg.CreateMap<SeatDto, Seat>();
                cfg.CreateMap<LayoutDto, Layout>();
                cfg.CreateMap<VenueDto, Venue>();
                cfg.CreateMap<EventAreaDto, EventArea>();
                cfg.CreateMap<EventSeatDto, EventSeat>();
            }).CreateMapper();
            return mapper;
        }
    }
}