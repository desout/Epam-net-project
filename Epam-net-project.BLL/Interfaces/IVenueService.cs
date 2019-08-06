using BLL.Models;

namespace BLL.Interfaces
{
    public interface IVenueService
    {
        int CreateVenue(VenueDto venue);
        int CreateSeat(SeatDto seat);
        int CreateLayout(LayoutDto layout);
    }
}
