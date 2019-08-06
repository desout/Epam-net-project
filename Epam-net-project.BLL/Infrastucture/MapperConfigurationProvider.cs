using AutoMapper;
using BLL.Models;
using DAL.models;

namespace BLL.Infrastucture
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
            }).CreateMapper();
            return mapper;
        }
    }
}