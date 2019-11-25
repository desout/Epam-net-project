using AutoMapper;

namespace EpamNetProject.PLL.Interfaces
{
    public interface IUserMapperConfigurationProvider
    {
        IMapper GetMapperConfig();
    }
}