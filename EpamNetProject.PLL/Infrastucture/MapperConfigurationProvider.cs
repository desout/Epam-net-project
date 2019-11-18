using AutoMapper;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Infrastucture
{
    internal static class MapperConfigurationProvider
    {
        public static IMapper GetMapperConfig()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserProfile, UserProfileDTO>();
            }).CreateMapper();
            return mapper;
        }
    }
}
