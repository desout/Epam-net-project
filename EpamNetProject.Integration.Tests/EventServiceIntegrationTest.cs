using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Transactions;
using EpamNetProject.BLL.Infrastructure;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Models;
using EpamNetProject.DAL.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace EpamNetProject.Integration.Tests
{
    [TestFixture]
    public class EventServiceIntegrationTest
    {
        [SetUp]
        public void SetUp()
        {
            var sqlConnectionString =
                ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            var context = new MyContext(sqlConnectionString);

            var eventRepository = new EventRepository(context);
            var layoutRepository = new Repository<Layout>(context);
            var areaRepository = new Repository<Area>(context);
            var seatRepository = new Repository<Seat>(context);
            var eventSeatRepository = new Repository<EventSeat>(context);
            var eventAreaRepository = new Repository<EventArea>(context);
            var userProfileRepository = new Repository<UserProfile>(context);
            var mapper = new MapperConfigurationProvider();
            _eventService = new EventService(eventRepository, layoutRepository,
                areaRepository, seatRepository, eventSeatRepository, eventAreaRepository, userProfileRepository,
                15, mapper);
        }

        private EventService _eventService;

        [Test]
        public void CreateEvent_WhenEventWithSameTimeExists_ShouldReturnSameTimeValidationException()
        {
            using (new TransactionScope())
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
            using (new TransactionScope())
            {
                var sEvent = new EventDto
                {
                    Name = "New Event", Description = "Description", LayoutId = 1,
                    EventDate = DateTime.Today.Add(TimeSpan.FromDays(23)), ImgUrl = ""
                };

                var result = _eventService.CreateEvent(sEvent);
                sEvent.Should().BeEquivalentTo(_eventService.GetEvent(result), options =>
                {
                    options.Excluding(x => x.EventAvailability);
                    return options.Excluding(x => x.Id);
                });
            }
        }

        [Test]
        public void CreateEvent_WhenSeatsNotExists_ShouldReturnNoSeatsValidationException()
        {
            using (new TransactionScope())
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
