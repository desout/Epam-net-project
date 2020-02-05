using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.BLL.Infrastucture
{
    public class MapperConfigurationProvider : IMapperConfigurationProvider
    {
        public IMapper GetMapperConfig()
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
                cfg.CreateMap<EventArea, Area>();
                cfg.CreateMap<Area, EventArea>();
                cfg.CreateMap<EventSeat, Seat>();
                cfg.CreateMap<Seat, EventSeat>();
                cfg.CreateMap<User, UserDTO>().ForMember(dest => dest.Role,
                        opt => opt.MapFrom(src => ""))
                    .ForMember(dest => dest.Role,
                        opt => opt.MapFrom(src => new UserProfileDTO()));
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
                cfg.CreateMap<UserProfile, UserProfileDTO>();
            }).CreateMapper();
            return mapper;
        }
    }
}