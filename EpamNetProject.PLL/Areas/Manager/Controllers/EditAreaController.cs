using System;
using System.Data.Entity.Core;
using System.Web.Mvc;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Helpers;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Models;

namespace EpamNetProject.PLL.Areas.Manager.Controllers
{
    public class EditAreaController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;

        public EditAreaController(IEventService eventService, IVenueService venueService, IUserMapperConfigurationProvider userMapperConfigurationProvider)
        {
            _eventService = eventService;
            _venueService = venueService;
            _mapper = userMapperConfigurationProvider.GetMapperConfig();
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddArea(AddAreaModel area)
        {
            if (!ModelState.IsValid)
            {
                return HttpResponseHelper.Error();
            }

            var baseArea = _eventService.CreateEventArea(_mapper.Map<EventAreaDto>(area));
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
                var baseSeat = _eventService.AddSeat(_mapper.Map<EventSeatDto>(model));
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
                var baseSeat = _venueService.AddSeat(_mapper.Map<SeatDto>(model));
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

            var baseArea = _venueService.CreateArea(_mapper.Map<AreaDto>(area));
            return HttpResponseHelper.Ok(new {Area = baseArea});
        }
    }
}
