using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.filters;
using EpamNetProject.PLL.Jobs;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

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
                seats = _eventService.GetSeatsByEvent(id, User.Identity.GetUserId()).ToList();
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
            try
            {
                var reserveDate = _eventService.ReserveSeat(id, User.Identity.GetUserId());
                var delay = int.Parse(ConfigurationManager.AppSettings["ReserveTime"]);
                var expires = delay - TimeSpan.FromTicks(DateTime.UtcNow.Ticks - reserveDate.Value.Ticks).TotalMinutes;
                var returnedValue = reserveDate.Value
                    .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                    .TotalMilliseconds;
                var cookie = new HttpCookie("reserveDate")
                    {HttpOnly = false, Value = returnedValue.ToString(), Expires = DateTime.Now.AddMinutes(expires)};
                Response.Cookies.Set(cookie);
                ConfigureScheduler();
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [Authorize]
        public ActionResult ProceedToCheckout()
        {
            var userId = User.Identity.GetUserId();
            return View(_eventService.GetReservedSeatByUser(userId).GroupBy(x => x.EventName));
        }

        [Authorize]
        public ActionResult Buy(decimal totalAmount)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                var countOfTickets = _eventService.ChangeStatusToBuy(userId, totalAmount);
                return View("PaymentSuccess", countOfTickets);
            }
            catch(Exception e)
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
                var reserveDate = _eventService.UnReserveSeat(id, User.Identity.GetUserId());
                if (reserveDate.HasValue)
                {
                    var delay = int.Parse(ConfigurationManager.AppSettings["ReserveTime"]);
                    var expires = delay - TimeSpan.FromTicks(DateTime.UtcNow.Ticks - reserveDate.Value.Ticks)
                                      .TotalMinutes;
                    var returnedValue = reserveDate.Value
                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds;
                    var cookie = new HttpCookie("reserveDate")
                    {
                        HttpOnly = false, Value = returnedValue.ToString(), Expires = DateTime.Now.AddMinutes(expires)
                    };
                    Response.Cookies.Set(cookie);
                }
                else
                {
                    Response.Cookies["reserveDate"].Expires = DateTime.Now.AddDays(-1);
                }

                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        private void ConfigureScheduler()
        {
            _basketScheduler.Start(User.Identity.GetUserId());
        }
    }
}