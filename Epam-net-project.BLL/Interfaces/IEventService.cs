﻿using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventDto Event);

        int UpdateEvent(EventDto Event);

        bool ReserveSeat(int id, string userId);

        bool UnReserveSeat(int id, string userId);

        EventDto GetEvent(int id);

        List<EventDto> GetAllEvents();

        int GetAvailabilityPercentage(int layoutId);

        IEnumerable<EventAreaDto> GetAreasByEvent(int eventId);

        IEnumerable<EventSeatDto> GetSeatsByUser(string userId);

        List<EventAreaDto> GetAllAreas();

        IEnumerable<EventSeatDto> GetSeatsByEvent(int eventId);

        List<EventDto> GetUserPurchaseHistory(string userId);

        bool ChangeStatusToBuy(string userId, decimal totalAmount);

        int RemoveEvent(int id);

        List<PriceSeat> GetReservedSeatByUser(string userId);

        void CheckReservation();
    }
}
