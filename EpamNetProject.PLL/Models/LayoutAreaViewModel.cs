using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Models
{
    public class LayoutAreaViewModel
    {
        public List<AreaDto> Areas;

        public int LayoutId;

        public List<SeatDto> Seats;

        public LayoutAreaViewModel()
        {
            Areas = new List<AreaDto>();
            Seats = new List<SeatDto>();
        }
    }
}