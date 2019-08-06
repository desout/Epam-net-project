using System;
using System.Linq;
using AutoMapper;
using BLL.Infrastucture;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.models;

namespace BLL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ILayoutRepository _layoutRepository;
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IVenueRepository _venueRepository;

        public VenueService(ISeatRepository seatRepository, ILayoutRepository layoutRepository,
            IVenueRepository venueRepository, IAreaRepository areaRepository)
        {
            _venueRepository = venueRepository;
            _layoutRepository = layoutRepository;
            _seatRepository = seatRepository;
            _areaRepository = areaRepository;
            _mapper = MapperConfigurationProvider.GetMapperConfig();
        }

        public int CreateVenue(VenueDto venue)
        {
            if (IsVenueExist(venue)) throw new Exception("Venue can't be created for this name");

            return _venueRepository.Add(_mapper.Map<Venue>(venue));
        }

        public int CreateLayout(LayoutDto layout)
        {
            if (IsLayoutExist(layout)) throw new Exception("Layout can't be created for this name");

            return _layoutRepository.Add(_mapper.Map<Layout>(layout));
        }

        public int CreateSeat(SeatDto seat)
        {
            if (IsSeatExist(seat)) throw new Exception("Seat can't be created with this seat and row");

            return _seatRepository.Add(_mapper.Map<Seat>(seat));
        }

        public int CreateArea(AreaDto area)
        {
            if (IsAreaExist(area)) throw new Exception("Area can't be created with this description");

            return _areaRepository.Add(_mapper.Map<Area>(area));
        }

        private bool IsAreaExist(AreaDto area)
        {
            return _areaRepository.GetAll().Any(x => x.Description == area.Description);
        }

        private bool IsSeatExist(SeatDto seat)
        {
            return _seatRepository.GetAll().Any(x => x.Number == seat.Number && x.Row == seat.Row);
        }

        private bool IsLayoutExist(LayoutDto layout)
        {
            return _layoutRepository.GetAll()
                .Any(x => x.VenueId == layout.VenueId && x.LayoutName == layout.LayoutName);
        }

        private bool IsVenueExist(VenueDto venue)
        {
            return _venueRepository.GetAll().Any(x => x.Name == venue.Name);
        }
    }
}
