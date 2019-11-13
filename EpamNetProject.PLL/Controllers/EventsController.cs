using System.Linq;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;

        public EventsController(IEventService eventService, IVenueService venueService)
        {
            _eventService = eventService;
            _venueService = venueService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var eventList = _eventService.GetAllEvents();
            return View(eventList);
        }

        [HttpGet]
        public ActionResult Event(int id)
        {
            var returnedEvent = _eventService.GetEvent(id);
            var seats = _eventService.GetSeatsByEvent(id);
            var areas = _eventService.GetAreasByEvent(id);
            var returnedModel = new EventViewModel
            {
                Event = returnedEvent,
                EventSeats = seats,
                EventAreas = areas
            };
            return View(returnedModel);
        }

        [HttpPost]
        public JsonResult Select(int id)
        {
            var success = _eventService.ReserveSeat(id, User.Identity.GetUserId());
            return Json(success);
        }

        public ActionResult ProceedToCheckout()
        {
            var userId = User.Identity.GetUserId();
            var seats = _eventService.GetSeatsByUser(userId).Where(x => x.State == 1 && x.UserId == userId).ToList();
            var areas = _eventService.GetAllAreas().Join(seats, x => x.Id, c => c.EventAreaId,
                (x, c) => new PriceSeat {Seat = c, Price = x.Price});
            return View(areas);
        }

        public ActionResult Buy()
        {
            var userId = User.Identity.GetUserId();
            var seats = _eventService.GetSeatsByUser(userId).Where(x => x.State == 1 && x.UserId == userId).ToList();
            var isSuccess = _eventService.ChangeStatusToBuy(seats);
            if (isSuccess)
            {
                
                return View("PaymentSuccess");
            }

            return View("PaymentFailed");
        }
    }
}
