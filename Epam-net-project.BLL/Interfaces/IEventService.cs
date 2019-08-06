using BLL.Models;

namespace BLL.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventDto Event);
    }
}
