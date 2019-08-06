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
    public class EventService : IEventService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILayoutRepository _layoutRepository;
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;

        public EventService(IEventRepository eventRepository, ILayoutRepository layoutRepository,
            IAreaRepository areaRepository, ISeatRepository seatRepository)
        {
            _eventRepository = eventRepository;
            _layoutRepository = layoutRepository;
            _areaRepository = areaRepository;
            _seatRepository = seatRepository;
            _mapper = MapperConfigurationProvider.GetMapperConfig();
        }

        public int CreateEvent(EventDto Event)
        {
            if (Event.EventDate.CompareTo(DateTime.Now) < 0) throw new Exception("Event can't be added in past");

            if (IsEventExist(Event)) throw new Exception("Event can't be created for one venue in the same time");

            if (!IsSeatsExist(Event)) throw new Exception("Event can't be created due to no seats exist");

            return _eventRepository.Add(_mapper.Map<Event>(Event));
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