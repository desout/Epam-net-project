using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventDto Event);
    }
}