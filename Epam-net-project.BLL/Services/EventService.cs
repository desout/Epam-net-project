using System;
using System.Collections.Generic;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Area> _areaRepository;

        private readonly IRepository<EventArea> _eventAreaRepository;

        private readonly IEventRepository _eventRepository;

        private readonly IRepository<EventSeat> _eventSeatRepository;

        private readonly IRepository<Layout> _layoutRepository;

        private readonly IMapper _mapper;

        private readonly int _reserveTime;

        private readonly IRepository<Seat> _seatRepository;

        private readonly IRepository<UserProfile> _userProfileRepository;

        public EventService(IEventRepository eventRepository, IRepository<Layout> layoutRepository,
            IRepository<Area> areaRepository, IRepository<Seat> seatRepository,
            IRepository<EventSeat> eventSeatRepository, IRepository<EventArea> eventAreaRepository,
            IRepository<UserProfile> userProfileRepository, int reserveTime,
            IMapperConfigurationProvider mapperConfigurationProvider)
        {
            _eventRepository = eventRepository;
            _layoutRepository = layoutRepository;
            _areaRepository = areaRepository;
            _seatRepository = seatRepository;
            _eventSeatRepository = eventSeatRepository;
            _eventAreaRepository = eventAreaRepository;
            _userProfileRepository = userProfileRepository;
            _mapper = mapperConfigurationProvider.GetMapperConfig();
            _reserveTime = reserveTime;
        }

        public int UpdateEvent(EventDto Event)
        {
            if (_eventRepository.Get(Event.Id).LayoutId == Event.LayoutId)
            {
                return _eventRepository.Update(_mapper.Map<Event>(Event));
            }

            _eventAreaRepository.GetAll().Where(x => x.EventId == Event.Id)
                .Map(x => _eventAreaRepository.Remove(x.Id));
            var areas = _areaRepository.GetAll().Where(x => x.LayoutId == Event.LayoutId).ToList();
            areas.Map(x => _eventAreaRepository.Add(_mapper.Map<EventArea>(x)));
            _seatRepository.GetAll().Join(areas, x => x.AreaId, a => a.Id, (x, a) => x)
                .Map(x => _eventSeatRepository.Add(_mapper.Map<EventSeat>(x)));

            return _eventRepository.Update(_mapper.Map<Event>(Event));
        }

        public DateTime? ReserveSeat(int id, string userId)
        {
            var eventSeat = _eventSeatRepository.Get(id);
            if (eventSeat.State != 0)
            {
                throw new DataException();
            }

            eventSeat.State = SeatStatus.Reserved;
            eventSeat.UserId = userId;
            _eventSeatRepository.Update(eventSeat);
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            if (userProfile == null)
            {
                throw new EntityException();
            }

            if (userProfile.ReserveDate != null)
            {
                return userProfile.ReserveDate;
            }

            userProfile.ReserveDate = DateTime.UtcNow;
            _userProfileRepository.Update(userProfile);

            return userProfile.ReserveDate;
        }

        public DateTime? UnReserveSeat(int id, string userId)
        {
            var eventSeat = _eventSeatRepository.Get(id);
            eventSeat.State = 0;
            eventSeat.UserId = null;
            _eventSeatRepository.Update(eventSeat);

            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            if (userProfile == null)
            {
                throw new EntityException();
            }

            if (_eventSeatRepository.GetAll().Where(x => x.UserId == userId).ToList().Count != 0)
            {
                return userProfile.ReserveDate;
            }

            userProfile.ReserveDate = null;
            _userProfileRepository.Update(userProfile);


            return null;
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
            {
                throw new ValidationException("Event can't be added in past");
            }

            if (IsEventExist(Event))
            {
                throw new ValidationException("Event can't be created for one venue in the same time");
            }

            if (!IsSeatsExist(Event))
            {
                throw new ValidationException("Event can't be created due to no seats exist");
            }

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

            return seats.Count() / availableSeatsCount * 100;
        }

        public IEnumerable<EventAreaDto> GetAreasByEvent(int eventId)
        {
            return _mapper.Map<List<EventAreaDto>>(_eventAreaRepository.GetAll().Where(x => x.EventId == eventId)
                .AsEnumerable());
        }

        public IEnumerable<EventSeatDto> GetSeatsByEvent(int eventId)
        {
            return _mapper.Map<List<EventSeatDto>>(_eventSeatRepository.GetAll().Join(
                _eventAreaRepository.GetAll().Where(a => a.EventId == eventId),
                s => s.EventAreaId,
                a => a.Id,
                (s, a) => s).AsEnumerable());
        }

        public IEnumerable<EventSeatDto> GetSeatsByUser(string userId)
        {
            return _mapper.Map<List<EventSeatDto>>(_eventSeatRepository.GetAll().Where(x => x.UserId == userId)
                .AsEnumerable());
        }

        public List<EventAreaDto> GetAllAreas()
        {
            return _mapper.Map<List<EventAreaDto>>(_eventAreaRepository.GetAll().AsEnumerable());
        }

        public List<EventDto> GetUserPurchaseHistory(string userId)
        {
            var events = _eventSeatRepository.GetAll().AsEnumerable().Where(x => x.UserId == userId).Join(
                _eventAreaRepository.GetAll().AsEnumerable()
                    .Join(_eventRepository.GetAll(),
                        s => s.EventId,
                        a => a.Id,
                        (s, a) => a),
                s => s.EventAreaId,
                a => a.Id,
                (s, a) => a).ToList();

            return _mapper.Map<List<EventDto>>(events);
        }

        public bool ChangeStatusToBuy(string userId, decimal totalAmount)
        {
            var seats = _eventSeatRepository.GetAll().Where(x => x.State == SeatStatus.Reserved && x.UserId == userId)
                .ToList();

            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            if (userProfile != null && userProfile.Balance < totalAmount)
            {
                return false;
            }

            foreach (var seat in seats)
            {
                seat.State = SeatStatus.Bought;
                _eventSeatRepository.Update(_mapper.Map<EventSeat>(seat));
            }

            if (userProfile == null)
            {
                return true;
            }

            userProfile.Balance -= totalAmount;
            _userProfileRepository.Update(userProfile);

            return true;
        }

        public int RemoveEvent(int id)
        {
            return IsSeatsBought(id) ? -1 : _eventRepository.Remove(id);
        }

        public List<PriceSeat> GetReservedSeatByUser(string userId)
        {
            var seats = _eventSeatRepository.GetAll().Where(x => x.State == SeatStatus.Reserved && x.UserId == userId);
            var events = _eventRepository.GetAll().ToList();
            return _eventAreaRepository.GetAll().Join(seats, x => x.Id, c => c.EventAreaId,
                (x, c) => new
                {
                    Seat = c, x.Price, AreaName = x.Description, x.EventId
                }).AsEnumerable().Select(x => new PriceSeat
            {
                Seat = _mapper.Map<EventSeatDto>(x.Seat), Price = x.Price, AreaName = x.AreaName,
                EventName = events.Find(e => e.Id == x.EventId).Name
            }).ToList();
        }

        public void CheckReservation(string userId)
        {
            var profile = _userProfileRepository.GetAll().Where(x => x.UserId == userId).AsEnumerable().FirstOrDefault(
                x => x.ReserveDate.HasValue && x.ReserveDate.Value.AddMinutes(_reserveTime) < DateTime.UtcNow);

            UpdateSeats(profile);
            profile.ReserveDate = null;
            _userProfileRepository.Update(profile);
        }

        public void CheckReservationAll()
        {
            var profiles = _userProfileRepository.GetAll().AsEnumerable().Where(x =>
                x.ReserveDate.HasValue && x.ReserveDate.Value.AddMinutes(_reserveTime) < DateTime.UtcNow).ToList();
            foreach (var profile in profiles)
            {
                UpdateSeats(profile);
                profile.ReserveDate = null;
                _userProfileRepository.Update(profile);
            }
        }

        public EventAreaDto CreateEventArea(EventAreaDto eventArea)
        {
            var id = _eventAreaRepository.Add(_mapper.Map<EventArea>(eventArea));

            eventArea.Id = id;
            return eventArea;
        }

        public int UpdateEventArea(EventAreaDto eventArea)
        {
            var baseArea = _eventAreaRepository.Get(eventArea.Id);
            if (!baseArea.Price.Equals(eventArea.Price))
            {
                if (IsSeatsBoughtByArea(eventArea.Id))
                {
                    throw new ValidationException("Price cannot be changed if some seats bought");
                }
            }

            if (eventArea.CoordX.HasValue)
            {
                baseArea.CoordX = eventArea.CoordX.Value;
            }

            if (eventArea.CoordY.HasValue)
            {
                baseArea.CoordY = eventArea.CoordY.Value;
            }

            baseArea.Description = eventArea.Description;
            return _eventAreaRepository.Update(baseArea);
        }

        public int RemoveSeat(int id)
        {
            if (_eventSeatRepository.Get(id).State == SeatStatus.Free)
            {
                return _eventSeatRepository.Remove(id);
            }

            return -1;
        }

        public EventSeatDto AddSeat(EventSeatDto eventSeatDto)
        {
            if (_eventSeatRepository.GetAll().Any(x =>
                x.EventAreaId == eventSeatDto.EventAreaId && x.Number == eventSeatDto.Number &&
                x.Row == eventSeatDto.Row))
            {
                throw new EntityException("This seat is exist now");
            }

            var id = _eventSeatRepository.Add(_mapper.Map<EventSeat>(eventSeatDto));

            eventSeatDto.Id = id;
            return eventSeatDto;
        }

        private bool IsSeatsExist(EventDto _event)
        {
            return _seatRepository.GetAll().Join(_areaRepository.GetAll().Where(a => a.LayoutId == _event.LayoutId),
                s => s.AreaId,
                a => a.Id,
                (s, a) => s).Any();
        }

        private bool IsSeatsBought(int id)
        {
            return _eventSeatRepository.GetAll().Join(_eventAreaRepository.GetAll().Where(x => x.EventId == id),
                s => s.EventAreaId, a => a.Id, (s, a) => s).Any(x => x.State != SeatStatus.Free);
        }

        private bool IsSeatsBoughtByArea(int id)
        {
            return _eventSeatRepository.GetAll().Where(x => x.EventAreaId == id).Any(x => x.State != SeatStatus.Free);
        }

        private void UpdateSeats(UserProfile profile)
        {
            var seats = _eventSeatRepository.GetAll()
                .Where(x => x.State == SeatStatus.Reserved && x.UserId == profile.UserId).ToList();
            foreach (var seat in seats)
            {
                seat.State = 0;
                seat.UserId = null;
                _eventSeatRepository.Update(_mapper.Map<EventSeat>(seat));
            }
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