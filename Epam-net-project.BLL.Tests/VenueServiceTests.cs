using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using Moq;
using NUnit.Framework;

namespace EpamNetProject.BLL.Tests
{
    [TestFixture]
    public class VenueServiceTests
    {
        private const int ReturnIdVenue = 10;
        private const int ReturnIdSeat = 20;
        private const int ReturnIdLayout = 30;
        private const int ReturnIdArea = 40;
        private Mock<IAreaRepository> _areaRepository;
        private Mock<ILayoutRepository> _layoutRepository;
        private Mock<ISeatRepository> _seatRepository;
        private Mock<IVenueRepository> _venueRepository;
        private VenueService _venueService;

        [SetUp]
        public void SetUp()
        {
            _venueRepository = new Mock<IVenueRepository>();
            _layoutRepository = new Mock<ILayoutRepository>();
            _areaRepository = new Mock<IAreaRepository>();
            _seatRepository = new Mock<ISeatRepository>();

            _venueRepository.Setup(x => x.GetAll())
                .Returns(new List<Venue>
                {
                    new Venue
                    {
                        Id = 1, Name = "First Venue", Description = "Description", Address = "Address",
                        Phone = "8-800-555-35-35"
                    },
                    new Venue
                    {
                        Id = 2, Name = "Second Venue", Description = "Description", Address = "Address",
                        Phone = "8-800-555-35-35"
                    },
                    new Venue
                    {
                        Id = 3, Name = "Third Venue", Description = "Description", Address = "Address",
                        Phone = "8-800-555-35-35"
                    },
                    new Venue
                    {
                        Id = 4, Name = "Fourth Venue", Description = "Description", Address = "Address",
                        Phone = "8-800-555-35-35"
                    },
                    new Venue
                    {
                        Id = 5, Name = "Fifth Venue", Description = "Description", Address = "Address",
                        Phone = "8-800-555-35-35"
                    }
                });
            _venueRepository.Setup(x => x.Add(It.IsAny<Venue>()))
                .Returns(ReturnIdVenue);
            _seatRepository.Setup(x => x.Add(It.IsAny<Seat>()))
                .Returns(ReturnIdSeat);
            _layoutRepository.Setup(x => x.Add(It.IsAny<Layout>()))
                .Returns(ReturnIdLayout);
            _areaRepository.Setup(x => x.Add(It.IsAny<Area>()))
                .Returns(ReturnIdArea);
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

            _venueService = new VenueService(_seatRepository.Object, _layoutRepository.Object,
                _venueRepository.Object, _areaRepository.Object);
        }

        [Test]
        public void CreateVenue_Success_ShouldReturnNewId()
        {
            var venue = new VenueDto
            {
                Name = "New Venue", Description = "Description", Address = "Address",
                Phone = "8-800-555-35-35"
            };

            var result = _venueService.CreateVenue(venue);

            Assert.AreEqual(result, ReturnIdVenue);
        }

        [Test]
        public void CreateVenue_Fail_VenueExistException()
        {
            var venue = new VenueDto
            {
                Name = "First Venue", Description = "Description", Address = "Address",
                Phone = "8-800-555-35-35"
            };

            var exception = Assert.Throws<ValidationException>(() => _venueService.CreateVenue(venue));

            Assert.AreEqual("Venue can't be created for this name", exception.Message);
        }

        [Test]
        public void CreateVenue_Fail_VenueNameRequired()
        {
            var venue = new VenueDto
            {
                Name = null,
                Description = "Description",
                Address = "Address",
                Phone = "8-800-555-35-35"
            };

            var exception = Assert.Throws<ArgumentException>(() => _venueService.CreateVenue(venue));

            Assert.AreEqual("The Name field is required.", exception.Message);
        }
        [Test]
        public void CreateLayout_Success_ShouldReturnNewId()
        {
            var layout = new LayoutDto
            { Description = "Description", LayoutName = "new layout name", VenueId = 1 };

            var result = _venueService.CreateLayout(layout);

            Assert.AreEqual(result, ReturnIdLayout);
        }

        [Test]
        public void CreateLayout_Fail_VenueExistException()
        {
            var layout = new LayoutDto
            { Description = "Description", LayoutName = "1 layout name", VenueId = 1 };

            var exception = Assert.Throws<ValidationException>(() => _venueService.CreateLayout(layout));

            Assert.AreEqual("Layout can't be created for this name", exception.Message);
        }

        [Test]
        public void CreateLayout_Fail_LayoutNameRequired()
        {
            var layout = new LayoutDto
            { Description = "Description", LayoutName = null, VenueId = 1 };

            var exception = Assert.Throws<ArgumentException>(() => _venueService.CreateLayout(layout));

            Assert.AreEqual("The LayoutName field is required.", exception.Message);
        }

        [Test]
        public void CreateArea_Success_ShouldReturnNewId()
        {
            var area = new AreaDto
                {Description = "new Description", CoordX = 10, CoordY = 20, LayoutId = 1};

            var result = _venueService.CreateArea(area);

            Assert.AreEqual(result, ReturnIdArea);
        }

        [Test]
        public void CreateArea_Fail_AreaExistException()
        {
            var area = new AreaDto
                {Description = "Description", CoordX = 10, CoordY = 20, LayoutId = 1};

            var exception = Assert.Throws<ValidationException>(() => _venueService.CreateArea(area));

            Assert.AreEqual("Area can't be created with this description", exception.Message);
        }

        [Test]
        public void CreateArea_Fail_AreaDescriptionRequired()
        {
            var area = new AreaDto
            { Description = null, CoordX = 10, CoordY = 20, LayoutId = 1 };

            var exception = Assert.Throws<ArgumentException>(() => _venueService.CreateArea(area));

            Assert.AreEqual("The Description field is required.", exception.Message);
        }

        [Test]
        public void CreateSeat_Success_ShouldReturnNewId()
        {
            var seat = new SeatDto
                {Number = 11, AreaId = 1, Row = 12};

            var result = _venueService.CreateSeat(seat);

            Assert.AreEqual(result, ReturnIdSeat);
        }

        [Test]
        public void CreateSeat_Fail_SeatExistException()
        {
            var seat = new SeatDto
                {Number = 10, AreaId = 1, Row = 1};

            var exception = Assert.Throws<ValidationException>(() => _venueService.CreateSeat(seat));

            Assert.AreEqual("Seat can't be created with this seat and row", exception.Message);
        }
    }
}
