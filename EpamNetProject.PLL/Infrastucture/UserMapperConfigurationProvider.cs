using AutoMapper;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Infrastucture
{
    public class UserMapperConfigurationProvider : IUserMapperConfigurationProvider
    {
        public IMapper GetMapperConfig()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                .ConvertUsing(s=>new User { Email = s.Email, UserName = s.UserName});
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            }).CreateMapper();
            return mapper;
        }


    }
}
