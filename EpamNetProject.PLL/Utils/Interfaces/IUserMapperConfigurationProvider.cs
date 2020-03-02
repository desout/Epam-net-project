using AutoMapper;

namespace EpamNetProject.PLL.Utils.Interfaces
{
    public interface IUserMapperConfigurationProvider
    {
        IMapper GetMapperConfig();
    }
}