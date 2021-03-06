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
    public class VenueServiceIntegrationTest
    {
        [SetUp]
        public void SetUp()
        {
            var sqlConnectionString =
                ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            var context = new MyContext(sqlConnectionString);
            var venueRepository = new Repository<Venue>(context);
            var layoutRepository = new Repository<Layout>(context);
            var areaRepository = new Repository<Area>(context);
            var seatRepository = new Repository<Seat>(context);
            var mapper = new MapperConfigurationProvider();
            _venueService = new VenueService(seatRepository, layoutRepository,
                venueRepository, areaRepository, mapper);
        }

        private VenueService _venueService;

        [Test]
        public void CreateArea_WhenAreaDescriptionExists_ShouldReturnValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var area = new AreaDto
                    {Description = "Description", CoordX = 10, CoordY = 20, LayoutId = 1};

                Assert.Throws<ValidationException>(() => _venueService.CreateArea(area));
            }
        }


        [Test]
        public void CreateArea_WhenModelValid_ShouldInsertNewArea()
        {
            using (var scope = new TransactionScope())
            {
                var area = new AreaDto
                    {Description = "new Description", CoordX = 10, CoordY = 20, LayoutId = 1};

                var result = _venueService.CreateArea(area);

                area.Should().BeEquivalentTo(_venueService.GetArea(result), options => options.Excluding(x => x.Id));
            }
        }

        [Test]
        public void CreateLayout_WhenLayoutNameExists_ShouldReturnValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var layout = new LayoutDto
                    {Description = "Description", LayoutName = "1 layout name", VenueId = 1};

                Assert.Throws<ValidationException>(() => _venueService.CreateLayout(layout));
            }
        }

        [Test]
        public void CreateLayout_WhenModelValid_ShouldInsertNewLayout()
        {
            using (var scope = new TransactionScope())
            {
                var layout = new LayoutDto
                    {Description = "Description", LayoutName = "new layout name", VenueId = 1};

                var result = _venueService.CreateLayout(layout);

                layout.Should()
                    .BeEquivalentTo(_venueService.GetLayout(result), options => options.Excluding(x => x.Id));
            }
        }


        [Test]
        public void CreateSeat_WhenModelValid_ShouldInsertNewSeat()
        {
            using (var scope = new TransactionScope())
            {
                var seat = new SeatDto
                    {Number = 11, AreaId = 1, Row = 12};

                var result = _venueService.CreateSeat(seat);

                seat.Should().BeEquivalentTo(_venueService.GetSeat(result), options => options.Excluding(x => x.Id));
            }
        }

        [Test]
        public void CreateSeat_WhenSeatWithSeatAndRowExists_ShouldReturnValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var seat = new SeatDto
                    {Number = 10, AreaId = 1, Row = 1};

                Assert.Throws<ValidationException>(() => _venueService.CreateSeat(seat));
            }
        }

        [Test]
        public void CreateVenue_WhenModelValid_ShouldInsertNewVenue()
        {
            using (var scope = new TransactionScope())
            {
                var venue = new VenueDto
                {
                    Name = "New Venue", Description = "Description", Address = "Address",
                    Phone = "8-800-555-35-35"
                };

                var result = _venueService.CreateVenue(venue);

                venue.Should().BeEquivalentTo(_venueService.GetVenue(result), options => options.Excluding(x => x.Id));
            }
        }

        [Test]
        public void CreateVenue_WhenVenueExists_ShouldReturnValidationException()
        {
            using (var scope = new TransactionScope())
            {
                var venue = new VenueDto
                {
                    Name = "First Venue", Description = "Description", Address = "Address",
                    Phone = "8-800-555-35-35"
                };

                Assert.Throws<ValidationException>(() => _venueService.CreateVenue(venue));
            }
        }
    }
}
