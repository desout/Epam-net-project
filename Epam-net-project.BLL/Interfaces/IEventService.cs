using System;
using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventDto Event);

        int UpdateEvent(EventDto Event);

        int UpdateEventArea(EventAreaDto area);

        DateTime? ReserveSeat(int id, string userId);

        DateTime? UnReserveSeat(int id, string userId);

        EventDto GetEvent(int id);

        List<EventDto> GetAllEvents();

        int GetAvailabilityPercentage(int areaId);

        IEnumerable<EventAreaDto> GetAreasByEvent(int eventId);

        IEnumerable<EventSeatDto> GetSeatsByUser(string userId);

        EventAreaDto CreateEventArea(EventAreaDto eventArea);

        List<EventAreaDto> GetAllAreas();

        IEnumerable<EventSeatDto> GetSeatsByEvent(int eventId, string userId);

        List<EventDto> GetUserPurchaseHistory(string userId);

        int ChangeStatusToBuy(string userId, decimal totalAmount);

        int RemoveEvent(int id);

        List<PriceSeat> GetReservedSeatByUser(string userId);

        void CheckReservation(string userId);

        void CheckReservationAll();

        int RemoveSeat(int id);

        EventSeatDto AddSeat(EventSeatDto eventSeatDto);
    }
}