using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Utils.Models
{
    public class EventViewModel
    {
        public EventDto Event;

        public List<EventAreaDto> EventAreas;

        public List<EventSeatDto> EventSeats;
    }
}