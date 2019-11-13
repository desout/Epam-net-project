using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Models
{
    public class PurchaseSeatViewModel
    {
        public List<PriceSeat> Seats;
    }

    public class PriceSeat
    {
        public EventSeatDto Seat;
        public decimal Price;
    }
}
