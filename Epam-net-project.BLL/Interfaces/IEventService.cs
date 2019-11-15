using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventDto Event);
        int UpdateEvent(EventDto Event);
        bool ReserveSeat(int id, string userId);
        bool UnReserveSeat(int id);
        EventDto GetEvent(int id);
        List<EventDto> GetAllEvents();
        int GetAvailabilityPercentage(int layoutId);
        List<EventAreaDto> GetAreasByEvent(int eventId);
        List<EventSeatDto> GetSeatsByUser(string userId);
        List<EventAreaDto> GetAllAreas();
        List<EventSeatDto> GetSeatsByEvent(int eventId);
        List<EventDto> GetUserPurchaseHistory(string userId);
        bool ChangeStatusToBuy(List<EventSeatDto> seats, string userId, decimal totalAmount);
        int RemoveEvent(int id);
        List<PriceSeat> GetReservedSeatByUser(string userId);
    }
}
