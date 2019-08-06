using System;
using System.Configuration;
using System.Transactions;
using EpamNetProject.BLL.Models;
using EpamNetProject.BLL.Services;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Repositories;
using NUnit.Framework;

namespace EpamNetProject.Integration.Tests
{
    public class VenueServiceIntegrationTest
    {
        private const int ReturnIdVenue = 6;
        private const int ReturnIdSeat = 11;
        private const int ReturnIdLayout = 4;
        private const int ReturnIdArea = 7;
        private IAreaRepository _areaRepository;
        private ILayoutRepository _layoutRepository;
        private ISeatRepository _seatRepository;
        private IVenueRepository _venueRepository;
        private VenueService _venueService;
        
        [SetUp]
        public void SetUp()
        {
            var sqlConnectionString = 
                ConfigurationManager.
                    ConnectionStrings["SqlConnectionString"].ConnectionString;
            
            _venueRepository = new VenueRepository(sqlConnectionString);
            _layoutRepository = new LayoutRepository(sqlConnectionString);
            _areaRepository = new AreaRepository(sqlConnectionString);
            _seatRepository = new SeatRepository(sqlConnectionString);
            
            _venueService = new VenueService(_seatRepository, _layoutRepository,
                _venueRepository, _areaRepository);
        }
        
        [Test]
        public void CreateVenue_Success_ShouldReturnNewId()
        {
            using (var scope = new TransactionScope())
            {
                var venue = new VenueDto
                {
                    Name = "New Venue", Description = "Description", Address = "Address",
                    Phone = "8-800-555-35-35"
                };

                var result = _venueService.CreateVenue(venue);

                Assert.AreEqual(result, ReturnIdVenue);
            }
        }

        [Test]
        public void CreateVenue_Fail_VenueExistException()
        {
            using (var scope = new TransactionScope())
            {
                var venue = new VenueDto
                {
                    Name = "First Venue", Description = "Description", Address = "Address",
                    Phone = "8-800-555-35-35"
                };

                var exception = Assert.Throws<Exception>(() => _venueService.CreateVenue(venue));

                Assert.AreEqual("Venue can't be created for this name", exception.Message);
            }
        }

        [Test]
        public void CreateLayout_Success_ShouldReturnNewId()
        {
            using (var scope = new TransactionScope())
            {
                var layout = new LayoutDto
                    {Description = "Description", LayoutName = "new layout name", VenueId = 1};

                var result = _venueService.CreateLayout(layout);

                Assert.AreEqual(result, ReturnIdLayout);
            }
        }

        [Test]
        public void CreateLayout_Fail_VenueExistException()
        {
            using (var scope = new TransactionScope())
            {
                var layout = new LayoutDto
                    {Description = "Description", LayoutName = "1 layout name", VenueId = 1};

                var exception = Assert.Throws<Exception>(() => _venueService.CreateLayout(layout));

                Assert.AreEqual("Layout can't be created for this name", exception.Message);
            }
        }

        [Test]
        public void CreateArea_Success_ShouldReturnNewId()
        {
            using (var scope = new TransactionScope())
            {
                var area = new AreaDto
                    {Description = "new Description", CoordX = 10, CoordY = 20, LayoutId = 1};

                var result = _venueService.CreateArea(area);

                Assert.AreEqual(result, ReturnIdArea);
            }
        }

        [Test]
        public void CreateArea_Fail_AreaExistException()
        {
            using (var scope = new TransactionScope())
            {
                var area = new AreaDto
                    {Description = "Description", CoordX = 10, CoordY = 20, LayoutId = 1};

                var exception = Assert.Throws<Exception>(() => _venueService.CreateArea(area));

                Assert.AreEqual("Area can't be created with this description", exception.Message);
            }
        }

        [Test]
        public void CreateSeat_Success_ShouldReturnNewId()
        {
            using (var scope = new TransactionScope())
            {
                var seat = new SeatDto
                    {Number = 11, AreaId = 1, Row = 12};

                var result = _venueService.CreateSeat(seat);

                Assert.AreEqual(result, ReturnIdSeat);
            }
        }

        [Test]
        public void CreateSeat_Fail_SeatExistException()
        {
            using (var scope = new TransactionScope())
            {
                var seat = new SeatDto
                    {Number = 10, AreaId = 1, Row = 1};

                var exception = Assert.Throws<Exception>(() => _venueService.CreateSeat(seat));

                Assert.AreEqual("Seat can't be created with this seat and row", exception.Message);
            }
        }
    }
}
