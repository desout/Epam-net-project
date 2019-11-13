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
using Ninject.Infrastructure.Language;

namespace EpamNetProject.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Area> _areaRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<Layout> _layoutRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<EventSeat> _eventSeatRepository;
        private readonly IRepository<EventArea> _eventAreaRepository;

        public EventService(IRepository<Event> eventRepository, IRepository<Layout> layoutRepository,
            IRepository<Area> areaRepository, IRepository<Seat> seatRepository,
            IRepository<EventSeat> eventSeatRepository, IRepository<EventArea> eventAreaRepository)
        {
            _eventRepository = eventRepository;
            _layoutRepository = layoutRepository;
            _areaRepository = areaRepository;
            _seatRepository = seatRepository;
            _eventSeatRepository = eventSeatRepository;
            _eventAreaRepository = eventAreaRepository;
            _mapper = MapperConfigurationProvider.GetMapperConfig();
        }

        public int UpdateEvent(EventDto Event)
        {
            return _eventRepository.Update(_mapper.Map<Event>(Event));
        }

        public bool ReserveSeat(int id, string userId)
        {
            var eventSeat = _eventSeatRepository.Get(id);
            if (eventSeat.State == 0)
            {
                eventSeat.State = 1;
                eventSeat.UserId = userId;
                _eventSeatRepository.Update(eventSeat);
                return true;
            }

            return false;
        }

        public EventDto GetEvent(int id)
        {
            var item = _mapper.Map<EventDto>(_eventRepository.Get(id));
            item.EventAvailability = GetAvailabilityPercentage(item.LayoutId);
            return item;
        }

        public List<EventDto> GetAllEvents()
        {
            var items = _mapper.Map<List<EventDto>>(_eventRepository.GetAll());
            foreach (var item in items)
            {
                item.EventAvailability = GetAvailabilityPercentage(item.LayoutId);
            }

            return items;
        }

        public int CreateEvent(EventDto Event)
        {
            var validationResult = ModelValidation.IsValidModel(Event);
            if (validationResult != null)
            {
                throw new ArgumentException(validationResult);
            }

            if (Event.EventDate.CompareTo(DateTime.Now) < 0)
                throw new ValidationException("Event can't be added in past");

            if (IsEventExist(Event))
                throw new ValidationException("Event can't be created for one venue in the same time");

            if (!IsSeatsExist(Event)) throw new ValidationException("Event can't be created due to no seats exist");

            return _eventRepository.Add(_mapper.Map<Event>(Event));
        }

        public int GetAvailabilityPercentage(int layoutId)
        {
            var seats = _eventSeatRepository.GetAll().Where(x => x.EventAreaId == layoutId).ToList();
            var availableSeatsCount = seats.Count(x => x.State == 0);
            if (availableSeatsCount == 0 || !seats.Any())
            {
                return 0;
            }

            return seats.Count() / availableSeatsCount;
        }

        public List<EventAreaDto> GetAreasByEvent(int eventId)
        {
            return _mapper.Map<List<EventAreaDto>>(_eventAreaRepository.GetAll().Where(x => x.EventId == eventId));
        }

        public List<EventSeatDto> GetSeatsByEvent(int eventId)
        {
            return _mapper.Map<List<EventSeatDto>>(_eventSeatRepository.GetAll().Join(
                _eventAreaRepository.GetAll().Where(a => a.EventId == eventId),
                s => s.EventAreaId,
                a => a.Id,
                (s, a) => s));
        }
        public List<EventSeatDto> GetSeatsByUser(string userId)
        {
            return _mapper.Map<List<EventSeatDto>>(_eventSeatRepository.GetAll().Where(x=>x.UserId==userId));
        }
        
        public List<EventAreaDto> GetAllAreas()
        {
            return _mapper.Map<List<EventAreaDto>>(_eventAreaRepository.GetAll());
        }
        public List<EventDto> GetUserPurchaseHistory(string userId)
        {
            var events = _eventSeatRepository.GetAll().Where(x => x.UserId == userId).Join(
                _eventAreaRepository.GetAll()
                    .Join(_eventRepository.GetAll(),
                        s => s.EventId,
                        a => a.Id,
                        (s, a) => a),
                s => s.EventAreaId,
                a => a.Id,
                (s, a) => a);

            return _mapper.Map<List<EventDto>>(events);
        }

        public bool ChangeStatusToBuy(List<EventSeatDto> seats)
        {
            foreach (var seat in seats)
            {
                seat.State = 2;
                _eventSeatRepository.Update(_mapper.Map<EventSeat>(seat));
            }

            return true;
        }

        public int RemoveEvent(int id)
        {
            return _eventRepository.Remove(id);
        }

        private bool IsSeatsExist(EventDto _event)
        {
            return _seatRepository.GetAll().Join(_areaRepository.GetAll().Where(a => a.LayoutId == _event.LayoutId),
                s => s.AreaId,
                a => a.Id,
                (s, a) => s).Any();
        }

        private bool IsEventExist(EventDto _event)
        {
            return _eventRepository.GetAll()
                .Join(_layoutRepository.GetAll(),
                    e => e.LayoutId,
                    l => l.Id,
                    (e, l) => e)
                .Any(e => e.EventDate == _event.EventDate);
        }
    }
}
