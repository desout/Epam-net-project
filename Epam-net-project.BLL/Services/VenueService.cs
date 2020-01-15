using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
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
            IRepository<Venue> venueRepository, IRepository<Area> areaRepository,
            IMapperConfigurationProvider mapperConfigurationProvider)
        {
            _venueRepository = venueRepository;
            _layoutRepository = layoutRepository;
            _seatRepository = seatRepository;
            _areaRepository = areaRepository;
            _mapper = mapperConfigurationProvider.GetMapperConfig();
        }

        public int CreateVenue(VenueDto venue)
        {
            var validationResult = ModelValidation.IsValidModel(venue);
            if (validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }

            if (IsVenueExist(venue))
            {
                throw new ValidationException("Venue can't be created for this name");
            }

            var ven = _mapper.Map<Venue>(venue);
            return _venueRepository.Add(ven);
        }

        public List<LayoutDto> GetLayouts()
        {
            return _mapper.Map<List<LayoutDto>>(_layoutRepository.GetAll());
        }

        public int CreateLayout(LayoutDto layout)
        {
            var validationResult = ModelValidation.IsValidModel(layout);
            if (validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }

            if (IsLayoutExist(layout))
            {
                throw new ValidationException("Layout can't be created for this name");
            }

            return _layoutRepository.Add(_mapper.Map<Layout>(layout));
        }

        public int CreateSeat(SeatDto seat)
        {
            var validationResult = ModelValidation.IsValidModel(seat);
            if (validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }

            if (IsSeatExist(seat))
            {
                throw new ValidationException("Seat can't be created with this seat and row");
            }

            return _seatRepository.Add(_mapper.Map<Seat>(seat));
        }

        public List<VenueDto> GetVenues()
        {
            return _mapper.Map<List<VenueDto>>(_venueRepository.GetAll());
        }

        public LayoutDto GetLayout(int id)
        {
            return _mapper.Map<LayoutDto>(_layoutRepository.Get(id));
        }

        public int UpdateLayout(LayoutDto layout)
        {
            return _layoutRepository.Update(_mapper.Map<Layout>(layout));
        }

        public List<AreaDto> GetAreasByLayout(int id)
        {
            return _mapper.Map<List<AreaDto>>(_areaRepository.GetAll().Where(x => x.LayoutId == id));
        }

        public List<SeatDto> GetSeatsByLayout(int id)
        {
            return _mapper.Map<List<SeatDto>>(_seatRepository.GetAll()
                .Join(_areaRepository.GetAll().Where(x => x.LayoutId == id), x => x.AreaId, a => a.Id, (x, a) => x));
        }

        public int RemoveSeat(int id)
        {
            return _seatRepository.Remove(id);
        }

        AreaDto IVenueService.CreateArea(AreaDto areaDto)
        {
            var id = _areaRepository.Add(_mapper.Map<Area>(areaDto));

            areaDto.Id = id;
            return areaDto;
        }

        public SeatDto AddSeat(SeatDto seatDto)
        {
            if (_seatRepository.GetAll().Any(x => x.AreaId == seatDto.AreaId && x.Number == seatDto.Number &&
                                                  x.Row == seatDto.Row))
            {
                throw new EntityException("This seat is exist now");
            }

            var id = _seatRepository.Add(_mapper.Map<Seat>(seatDto));

            seatDto.Id = id;
            return seatDto;
        }

        public int UpdateArea(AreaDto area)
        {
            var baseArea = _areaRepository.Get(area.Id);
            baseArea.Price = area.Price;
            baseArea.Description = area.Description;
            if (area.CoordX.HasValue)
            {
                baseArea.CoordX = area.CoordX.Value;
            }

            if (area.CoordY.HasValue)
            {
                baseArea.CoordY = area.CoordY.Value;
            }

            return _areaRepository.Update(baseArea);
        }

        public VenueDto GetVenue(int id)
        {
            return _mapper.Map<VenueDto>(_venueRepository.Get(id));
        }

        public SeatDto GetSeat(int id)
        {
            return _mapper.Map<SeatDto>(_seatRepository.Get(id));
        }

        public AreaDto GetArea(int id)
        {
            return _mapper.Map<AreaDto>(_areaRepository.Get(id));
        }

        public int CreateArea(AreaDto area)
        {
            var validationResult = ModelValidation.IsValidModel(area);
            if (validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }

            if (IsAreaExist(area))
            {
                throw new ValidationException("Area can't be created with this description");
            }

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