using System.Collections.Generic;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IVenueService
    {
        int CreateVenue(VenueDto venue);

        int CreateSeat(SeatDto seat);

        int CreateLayout(LayoutDto layout);

        LayoutDto GetLayout(int id);

        List<LayoutDto> GetLayouts();

        List<VenueDto> GetVenues();

        int UpdateLayout(LayoutDto layout);

        IEnumerable<AreaDto> GetAreasByLayout(int id);

        IEnumerable<SeatDto> GetSeatsByLayout(int id);

        int RemoveSeat(int id);

        AreaDto CreateArea(AreaDto areaDto);

        SeatDto AddSeat(SeatDto seatDto);

        int UpdateArea(AreaDto area);
    }
}