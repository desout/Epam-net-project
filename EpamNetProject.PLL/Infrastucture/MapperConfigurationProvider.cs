using AutoMapper;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Infrastucture
{
    public class MapperConfigurationProvider: IUserMapperConfigurationProvider
    {
        public IMapper GetMapperConfig()
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
