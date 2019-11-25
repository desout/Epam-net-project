using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Models
{
    public class EventViewModel
    {
        public EventDto Event;

        public List<EventAreaDto> EventAreas;

        public List<EventSeatDto> EventSeats;
    }
}