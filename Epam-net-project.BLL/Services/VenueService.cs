using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.BLL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Area> _areaRepository;
        private readonly IRepository<Layout> _layoutRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<Venue> _venueRepository;

        public VenueService(IRepository<Seat> seatRepository, IRepository<Layout> layoutRepository,
            IRepository<Venue> venueRepository, IRepository<Area> areaRepository)
        {
            _venueRepository = venueRepository;
            _layoutRepository = layoutRepository;
            _seatRepository = seatRepository;
            _areaRepository = areaRepository;
            _mapper = MapperConfigurationProvider.GetMapperConfig();
        }
        public VenueDto GetVenue(int id) => _mapper.Map<VenueDto>(_venueRepository.Get(id));

        public int CreateVenue(VenueDto venue)
        {
            var validationResult = ModelValidation.IsValidModel(venue);
            if ( validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }
            if (IsVenueExist(venue)) throw new ValidationException("Venue can't be created for this name");
            var ven = _mapper.Map<Venue>(venue);
            return _venueRepository.Add(ven);
        }
        public LayoutDto GetLayout(int id) => _mapper.Map<LayoutDto>(_layoutRepository.Get(id));
        public List<LayoutDto> GetLayouts() => _mapper.Map<List<LayoutDto>>(_layoutRepository.GetAll());
        public int CreateLayout(LayoutDto layout)
        {
            var validationResult = ModelValidation.IsValidModel(layout);
            if ( validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }
            if (IsLayoutExist(layout)) throw new ValidationException("Layout can't be created for this name");

            return _layoutRepository.Add(_mapper.Map<Layout>(layout));
        }
        public SeatDto GetSeat(int id) => _mapper.Map<SeatDto>(_seatRepository.Get(id));
        public int CreateSeat(SeatDto seat)
        {
            var validationResult = ModelValidation.IsValidModel(seat);
            if ( validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }
            if (IsSeatExist(seat)) throw new ValidationException("Seat can't be created with this seat and row");

            return _seatRepository.Add(_mapper.Map<Seat>(seat));
        }
        public AreaDto GetArea(int id) => _mapper.Map<AreaDto>(_areaRepository.Get(id));

        public int CreateArea(AreaDto area)
        {
            var validationResult = ModelValidation.IsValidModel(area);
            if ( validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }
            if (IsAreaExist(area)) throw new ValidationException("Area can't be created with this description");

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
