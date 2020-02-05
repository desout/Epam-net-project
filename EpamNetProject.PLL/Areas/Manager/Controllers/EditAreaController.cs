using System;
using System.Data.Entity.Core;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Helpers;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Areas.Manager.Controllers
{
    public class EditAreaController: Controller
    {
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;

        public EditAreaController(IEventService eventService, IVenueService venueService)
        {
            _eventService = eventService;
            _venueService = venueService;

        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddArea(AddAreaModel area)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            var baseArea = _eventService.CreateEventArea(new EventAreaDto
            {
                Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                CoordY = area.LeftCorner.Y, EventId = area.EventId
            });
            return HttpResponseHelper.Ok(new {Area = baseArea});
        }
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult ChangeArea(EventAreaDto area)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            try
            {
                _eventService.UpdateEventArea(area);
                return HttpResponseHelper.Ok();
            }
            catch (Exception e)
            {
                return HttpResponseHelper.Error();
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult RemoveSeat(int id)
        {
            var removeId = _eventService.RemoveSeat(id);
            return removeId > 0 ? HttpResponseHelper.Ok() : HttpResponseHelper.Error();
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddSeat(AddSeatModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            try
            {
                var baseSeat = _eventService.AddSeat(new EventSeatDto
                {
                    Row = model.Row, Number = model.Number, EventAreaId = model.AreaId
                });
                return HttpResponseHelper.Ok(new {Seat = baseSeat});
            }
            catch (EntityException e)
            {
                return HttpResponseHelper.Error();
            }
        }
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult ChangeMainArea(AreaDto area)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            try
            {
                _venueService.UpdateArea(area);
                return HttpResponseHelper.Ok();
            }
            catch (Exception e)
            {
                return HttpResponseHelper.Error();
            }
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddMainSeat(AddSeatModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            try
            {
                var baseSeat = _venueService.AddSeat(new SeatDto
                {
                    Row = model.Row, Number = model.Number, AreaId = model.AreaId
                });
                return HttpResponseHelper.Ok(new {Seat = baseSeat});
            }
            catch (EntityException e)
            {
                return HttpResponseHelper.Error();
            }
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult RemoveMainSeat(int id)
        {
            var removeId = _venueService.RemoveSeat(id);
            return Json(removeId > 0);
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddMainArea(AddAreaModel area)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            var baseArea = _venueService.CreateArea(new AreaDto
            {
                Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                CoordY = area.LeftCorner.Y, LayoutId = area.EventId
            });
            return HttpResponseHelper.Ok(new {Area = baseArea});
        }
    }
}
