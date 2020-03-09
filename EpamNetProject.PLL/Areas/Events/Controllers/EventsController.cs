using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Extensions;
using EpamNetProject.PLL.Utils.filters;
using EpamNetProject.PLL.Utils.Helpers;
using EpamNetProject.PLL.Utils.Jobs;
using EpamNetProject.PLL.Utils.Models;

namespace EpamNetProject.PLL.Areas.Events.Controllers
{
    [ActionErrorLog]
    public class EventsController : Controller
    {
        private readonly BasketScheduler _basketScheduler;

        private readonly IEventService _eventService;

        public EventsController(IEventService eventService, BasketScheduler basketScheduler)
        {
            _eventService = eventService;
            _basketScheduler = basketScheduler;
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
                seats = _eventService.GetSeatsByEvent(id, User.GetUserId()).ToList();
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
        public ActionResult Select(int id)
        {
            try
            {
                var basketTime = _eventService.ReserveSeat(id, User.GetUserId());
                Response.Cookies.Set(basketTime.Value.ToJsCookieTime());
                ConfigureScheduler();
                return HttpResponseHelper.Ok();
            }
            catch (Exception e)
            {
                return HttpResponseHelper.Error();
            }
        }

        [Authorize]
        public ActionResult ProceedToCheckout()
        {
            var userId = User.GetUserId();
            return View(_eventService.GetReservedSeatByUser(userId).GroupBy(x => x.EventName));
        }

        [Authorize]
        public ActionResult Buy(decimal totalAmount)
        {
            var userId = User.GetUserId();
            try
            {
                var countOfTickets = _eventService.ChangeStatusToBuy(userId, totalAmount);
                return View("PaymentSuccess", countOfTickets);
            }
            catch (Exception e)
            {
                return View("PaymentFailed", e);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Deselect(int id)
        {
            try
            {
                var basketTime = _eventService.UnReserveSeat(id, User.GetUserId());
                if (basketTime.HasValue)
                {
                    Response.Cookies.Set(basketTime.Value.ToJsCookieTime());
                }
                else
                {
                    Response.Cookies["basketTime"].Expires = DateTime.Now.AddDays(-1);
                }

                return HttpResponseHelper.Ok();
            }
            catch
            {
                return HttpResponseHelper.Error();
            }
        }

        private void ConfigureScheduler()
        {
            _basketScheduler.Start(User.GetUserId());
        }
    }
}