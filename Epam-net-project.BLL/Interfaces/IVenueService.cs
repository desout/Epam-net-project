using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IVenueService
    {
        int CreateVenue(VenueDto venue);
        int CreateSeat(SeatDto seat);
        int CreateLayout(LayoutDto layout);
        List<LayoutDto> GetLayouts();
    }
}
