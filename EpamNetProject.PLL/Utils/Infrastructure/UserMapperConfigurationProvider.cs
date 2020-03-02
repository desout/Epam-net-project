using AutoMapper;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Models;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Models;

namespace EpamNetProject.PLL.Utils.Infrastructure
{
    public class UserMapperConfigurationProvider : IUserMapperConfigurationProvider
    {
        public IMapper GetMapperConfig()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                    .ConvertUsing(s => new User {Email = s.Email, UserName = s.UserName});
                cfg.CreateMap<AddAreaModel, EventAreaDto>()
                    .ConvertUsing(area => new EventAreaDto
                    {
                        Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                        CoordY = area.LeftCorner.Y, EventId = area.EventId
                    });
                cfg.CreateMap<AddSeatModel, EventSeatDto>()
                    .ConvertUsing(model => new EventSeatDto
                    {
                        Row = model.Row, Number = model.Number, EventAreaId = model.AreaId
                    });
                cfg.CreateMap<AddSeatModel, SeatDto>()
                    .ConvertUsing(model => new SeatDto
                    {
                        Row = model.Row, Number = model.Number, AreaId = model.AreaId
                    });
                cfg.CreateMap<AddAreaModel, AreaDto>()
                    .ConvertUsing(area => new AreaDto
                    {
                        Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                        CoordY = area.LeftCorner.Y, LayoutId = area.EventId
                    });
                cfg.CreateMap<EventDto, EditEventViewModel>()
                    .ConvertUsing(eventDto => new EditEventViewModel
                    {
                        Id = eventDto.Id,
                        Name = eventDto.Name,
                        Description = eventDto.Description,
                        Time = eventDto.EventDate,
                        Title = eventDto.Name,
                        ImgUrl = eventDto.ImgUrl,
                        Layout = eventDto.LayoutId
                    });
                cfg.CreateMap<LayoutDto, LayoutViewModel>()
                    .ConvertUsing(layoutDto => new LayoutViewModel
                    {
                        Id = layoutDto.Id,
                        VenueId = layoutDto.VenueId,
                        Description = layoutDto.Description,
                        LayoutName = layoutDto.LayoutName
                    });
                cfg.CreateMap<LayoutViewModel, LayoutDto>();
                cfg.CreateMap<EditEventViewModel, EventDto>()
                    .ForMember(x => x.EventDate, opt => opt.MapFrom(src => src.Time))
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Title))
                    .ForMember(x => x.LayoutId, opt => opt.MapFrom(src => src.Layout));
                cfg.CreateMap<UserDTO, UserProfileViewModel>()
                    .ConvertUsing(user => new UserProfileViewModel
                    {
                        Email = user.Email,
                        FirstName = user.UserProfile.FirstName,
                        Surname = user.UserProfile.Surname,
                        TimeZone = user.UserProfile.TimeZone,
                        Language = user.UserProfile.Language,
                        UserId = user.UserProfile.UserId
                    });
                cfg.CreateMap<UserProfileViewModel, UserDTO>()
                    .ConvertUsing(model => new UserDTO
                    {
                        Id = model.UserId,
                        Email = model.Email,
                        UserProfile = new UserProfileDTO
                        {
                            FirstName = model.FirstName,
                            Language = model.Language,
                            Surname = model.Surname,
                            TimeZone = model.TimeZone
                        }
                    });
                cfg.CreateMap<RegisterModel, UserDTO>()
                    .ConvertUsing(model => new UserDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                        UserName = model.UserName,
                        Role = "user",
                        UserProfile = new UserProfileDTO
                        {
                            Balance = 0,
                            FirstName = model.FirstName,
                            Language = model.Language,
                            basketTime = null,
                            Surname = model.Surname,
                            TimeZone = model.TimeZone
                        }
                    });
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            }).CreateMapper();
            return mapper;
        }
    }
}
