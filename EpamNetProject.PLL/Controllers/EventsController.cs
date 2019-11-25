using System.Collections.Generic;
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

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
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

            var seats = new List<EventSeatDto>();
            var areas = new List<EventAreaDto>();
            if (User.Identity.IsAuthenticated)
            {
                seats = _eventService.GetSeatsByEvent(id).ToList();
                areas = _eventService.GetAreasByEvent(id).ToList();
            }

            var returnedModel = new EventViewModel
            {
                Event = returnedEvent,
                EventSeats = seats,
                EventAreas = areas
            };
            return View(returnedModel);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Select(int id)
        {
            var success = _eventService.ReserveSeat(id, User.Identity.GetUserId());
            return Json(success);
        }

        [Authorize]
        public ActionResult ProceedToCheckout()
        {
            var userId = User.Identity.GetUserId();
            return View(_eventService.GetReservedSeatByUser(userId));
        }

        [Authorize]
        public ActionResult Buy(decimal totalAmount)
        {
            var userId = User.Identity.GetUserId();
            var isSuccess = _eventService.ChangeStatusToBuy(userId, totalAmount);
            return View(isSuccess ? "PaymentSuccess" : "PaymentFailed");
        }
    }
}
