using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Transactions;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Repositories;
using NUnit.Framework;

namespace EpamNetProject.Integration.Tests
{
    [TestFixture]
    public class EventServiceIntegrationTest
    {
        private IAreaRepository _areaRepository;
        private IEventRepository _eventRepository;
        private EventService _eventService;
        private ILayoutRepository _layoutRepository;
        private ISeatRepository _seatRepository;
        private const int ReturnId = 10;
        private const int EventListLength = 10;
        [SetUp]
        public void SetUp()
        {
            var sqlConnectionString =
                ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            _eventRepository = new EventRepository(sqlConnectionString);
            _layoutRepository = new LayoutRepository(sqlConnectionString);
            _areaRepository = new AreaRepository(sqlConnectionString);
            _seatRepository = new SeatRepository(sqlConnectionString);

            _eventService = new EventService(_eventRepository, _layoutRepository,
                _areaRepository, _seatRepository);
        }

        [Test]
        public void CreateEvent_Success_ShouldReturnNewId()
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
                Assert.AreSame(_eventService.GetEvent(result), sEvent);
            }
        }

        [Test]
        public void CreateEvent_Fail_SameTimeException()
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
        public void CreateEvent_Fail_NoSeats()
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
