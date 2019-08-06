using System;
using System.Collections.Generic;
using BLL.Models;
using BLL.Services;
using DAL.Interfaces;
using DAL.models;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class EventServiceTests
    {
        private const int ReturnId = 10;
        private Mock<IAreaRepository> _areaRepository;
        private Mock<IEventRepository> _eventRepository;
        private EventService _eventService;
        private Mock<ILayoutRepository> _layoutRepository;
        private Mock<ISeatRepository> _seatRepository;

        [SetUp]
        public void SetUp()
        {
            _eventRepository = new Mock<IEventRepository>();
            _layoutRepository = new Mock<ILayoutRepository>();
            _areaRepository = new Mock<IAreaRepository>();
            _seatRepository = new Mock<ISeatRepository>();

            _eventRepository.Setup(x => x.GetAll())
                .Returns(new List<Event>
                {
                    new Event
                    {
                        Id = 1, Name = "First Event", Description = "Description", LayoutId = 1,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(10))
                    },
                    new Event
                    {
                        Id = 2, Name = "Second Event", Description = "Description", LayoutId = 2,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(20))
                    },
                    new Event
                    {
                        Id = 3, Name = "Third Event", Description = "Description", LayoutId = 3,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(30))
                    },
                    new Event
                    {
                        Id = 4, Name = "Fourth Event", Description = "Description", LayoutId = 1,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(40))
                    },
                    new Event
                    {
                        Id = 5, Name = "Fifth Event", Description = "Description", LayoutId = 2,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(50))
                    },
                    new Event
                    {
                        Id = 6, Name = "Sixth Event", Description = "Description", LayoutId = 3,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(60))
                    },
                    new Event
                    {
                        Id = 7, Name = "Seventh Event", Description = "Description", LayoutId = 3,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(70))
                    },
                    new Event
                    {
                        Id = 8, Name = "Eighth Event", Description = "Description", LayoutId = 2,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(80))
                    },
                    new Event
                    {
                        Id = 9, Name = "Ninth Event", Description = "Description", LayoutId = 1,
                        EventDate = DateTime.Today.Add(TimeSpan.FromDays(90))
                    }
                });
            _eventRepository.Setup(x => x.Add(It.IsAny<Event>()))
                .Returns(ReturnId);
            _layoutRepository.Setup(x => x.GetAll())
                .Returns(new List<Layout>
                {
                    new Layout {Id = 1, Description = "Description", LayoutName = "1 layout name", VenueId = 1},
                    new Layout {Id = 2, Description = "Description", LayoutName = "2 layout name", VenueId = 2},
                    new Layout {Id = 3, Description = "Description", LayoutName = "3 layout name", VenueId = 3}
                });
            _areaRepository.Setup(x => x.GetAll())
                .Returns(new List<Area>
                {
                    new Area {Id = 1, Description = "Description", CoordX = 10, CoordY = 20, LayoutId = 1},
                    new Area {Id = 2, Description = "Description", CoordX = 20, CoordY = 30, LayoutId = 2},
                    new Area {Id = 3, Description = "Description", CoordX = 30, CoordY = 40, LayoutId = 2},
                    new Area {Id = 4, Description = "Description", CoordX = 40, CoordY = 50, LayoutId = 1},
                    new Area {Id = 5, Description = "Description", CoordX = 50, CoordY = 60, LayoutId = 3},
                    new Area {Id = 6, Description = "Description", CoordX = 60, CoordY = 70, LayoutId = 3}
                });
            _seatRepository.Setup(x => x.GetAll())
                .Returns(new List<Seat>
                {
                    new Seat {Id = 1, Number = 10, AreaId = 1, Row = 1},
                    new Seat {Id = 2, Number = 20, AreaId = 2, Row = 2},
                    new Seat {Id = 3, Number = 30, AreaId = 3, Row = 3},
                    new Seat {Id = 4, Number = 40, AreaId = 4, Row = 4},
                    new Seat {Id = 5, Number = 50, AreaId = 4, Row = 5},
                    new Seat {Id = 6, Number = 60, AreaId = 3, Row = 6},
                    new Seat {Id = 7, Number = 70, AreaId = 2, Row = 7},
                    new Seat {Id = 8, Number = 80, AreaId = 1, Row = 8},
                    new Seat {Id = 9, Number = 90, AreaId = 1, Row = 9},
                    new Seat {Id = 10, Number = 110, AreaId = 1, Row = 10}
                });

            _eventService = new EventService(_eventRepository.Object, _layoutRepository.Object,
                _areaRepository.Object, _seatRepository.Object);
        }

        [Test]
        public void CreateEvent_Success_ShouldReturnNewId()
        {
            var sEvent = new EventDto
            {
                Name = "New Event", Description = "Description", LayoutId = 1,
                EventDate = DateTime.Today.Add(TimeSpan.FromDays(1))
            };

            var result = _eventService.CreateEvent(sEvent);

            Assert.AreEqual(result, ReturnId);
        }

        [Test]
        public void CreateEvent_Fail_SameTimeException()
        {
            var sEvent = new EventDto
            {
                Name = "New Event", Description = "Description", LayoutId = 1,
                EventDate = DateTime.Today.Add(TimeSpan.FromDays(10))
            };

            var exception = Assert.Throws<Exception>(() => _eventService.CreateEvent(sEvent));

            Assert.AreEqual("Event can't be created for one venue in the same time", exception.Message);
        }

        [Test]
        public void CreateEvent_Fail_DateInPast()
        {
            var sEvent = new EventDto
            {
                Name = "New Event", Description = "Description", LayoutId = 1,
                EventDate = DateTime.Today.Subtract(TimeSpan.FromDays(20))
            };

            var exception = Assert.Throws<Exception>(() => _eventService.CreateEvent(sEvent));

            Assert.AreEqual("Event can't be added in past", exception.Message);
        }

        [Test]
        public void CreateEvent_Fail_NoSeats()
        {
            var sEvent = new EventDto
            {
                Name = "New Event", Description = "Description", LayoutId = 3,
                EventDate = DateTime.Today.Add(TimeSpan.FromDays(1))
            };

            var exception = Assert.Throws<Exception>(() => _eventService.CreateEvent(sEvent));

            Assert.AreEqual("Event can't be created due to no seats exist", exception.Message);
        }
    }
}
