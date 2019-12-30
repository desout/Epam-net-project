using System;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;

namespace EpamNetProject.Integration.Tests
{
    [TestFixture]
    public class EventServiceIntegrationTest
    {
        private IRepository<Area> _areaRepository;

        private IRepository<EventArea> _eventAreaRepository;

        private IEventRepository _eventRepository;

        private IRepository<EventSeat> _eventSeatRepository;

        private EventService _eventService;

        private IRepository<Layout> _layoutRepository;

        private IMapperConfigurationProvider _mapper;

        private IRepository<Seat> _seatRepository;

        private IRepository<UserProfile> _userProfileRepository;

        [SetUp]
        public void SetUp()
        {
            var sqlConnectionString =
                ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            var context = new MyContext(sqlConnectionString);

            _eventRepository = new EventRepository(context);
            _layoutRepository = new Repository<Layout>(context);
            _areaRepository = new Repository<Area>(context);
            _seatRepository = new Repository<Seat>(context);
            _eventSeatRepository = new Repository<EventSeat>(context);
            _eventAreaRepository = new Repository<EventArea>(context);
            _userProfileRepository = new Repository<UserProfile>(context);
            _mapper = new MapperConfigurationProvider();
            _eventService = new EventService(_eventRepository, _layoutRepository,
                _areaRepository, _seatRepository, _eventSeatRepository, _eventAreaRepository, _userProfileRepository,
                15, _mapper);
        }

        [Test]
        public void CreateEvent_WhenEventWithSameTimeExists_ShouldReturnSameTimeValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var sEvent = new EventDto
                {
                    Name = "New Event", Description = "Description", LayoutId = 1,
                    EventDate = DateTime.Today.Add(TimeSpan.FromDays(1))
                };

                var exception = Assert.Throws<ValidationException>(() => _eventService.CreateEvent(sEvent));

                Assert.AreEqual("Event can't be created for one venue in the same time", exception.Message);
            }
        }

        [Test]
        public void CreateEvent_WhenModelValid_ShouldInsertNewEvent()
        {
            using (var scope = new TransactionScope())
            {
                var sEvent = new EventDto
                {
                    Name = "New Event", Description = "Description", LayoutId = 1,
                    EventDate = DateTime.Today.Add(TimeSpan.FromDays(23))
                };

                var result = _eventService.CreateEvent(sEvent);
                sEvent.Id = result;
                sEvent.Should().BeEquivalentTo(_eventService.GetEvent(result));
            }
        }

        [Test]
        public void CreateEvent_WhenSeatsNotExists_ShouldReturnNoSeatsValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var sEvent = new EventDto
                {
                    Name = "New Event", Description = "Description", LayoutId = 3,
                    EventDate = DateTime.Today.Add(TimeSpan.FromDays(21))
                };

                var exception = Assert.Throws<ValidationException>(() => _eventService.CreateEvent(sEvent));

                Assert.AreEqual("Event can't be created due to no seats exist", exception.Message);
            }
        }
    }
}