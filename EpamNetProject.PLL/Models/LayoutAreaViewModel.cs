using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Models
{
    public class LayoutAreaViewModel
    {
        
        public List<AreaDto> Areas;

        public List<SeatDto> Seats;

        public int LayoutId;
        public LayoutAreaViewModel()
        {
            Areas = new List<AreaDto>();
            Seats = new List<SeatDto>();
        }
    }
}
