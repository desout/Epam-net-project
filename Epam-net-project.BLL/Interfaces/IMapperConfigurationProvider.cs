using AutoMapper;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IMapperConfigurationProvider
    {
        IMapper GetMapperConfig();
    }
}
