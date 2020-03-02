using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Extensions;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Models;

namespace EpamNetProject.PLL.Areas.Manager.Controllers
{
    public class EditLayoutController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IMapper _mapper;

        private readonly IVenueService _venueService;

        public EditLayoutController(IEventService eventService, IVenueService venueService,
            IUserMapperConfigurationProvider userMapperConfigurationProvider)
        {
            _eventService = eventService;
            _venueService = venueService;
            _mapper = userMapperConfigurationProvider.GetMapperConfig();
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditLayouts()
        {
            return View(_venueService.GetLayouts());
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditLayout(int? id)
        {
            ViewBag.Venues = _venueService.GetVenues();
            if (id == null)
            {
                return View(new LayoutViewModel());
            }

            var layoutDto = _venueService.GetLayout(id.Value);
            return View(_mapper.Map<LayoutViewModel>(layoutDto));
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddLayout(LayoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditLayout", model);
            }

            try
            {
                _venueService.CreateLayout(_mapper.Map<LayoutDto>(model));

                return RedirectToAction("EditLayouts");
            }
            catch (Exception)
            {
                // ignored
            }

            return View("EditLayout", model);
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult UpdateLayout(LayoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditLayout", model);
            }

            try
            {
                _venueService.UpdateLayout(_mapper.Map<LayoutDto>(model));

                return RedirectToAction("EditLayouts");
            }
            catch (Exception)
            {
                // ignored
            }

            return View("EditLayout", model);
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditLayoutForEvent(int id)
        {
            var returnedEvent = _eventService.GetEvent(id);

            var seats = _eventService.GetSeatsByEvent(id, User.GetUserId()).ToList();
            var areas = _eventService.GetAreasByEvent(id).ToList();

            var returnedModel = new EventViewModel
            {
                Event = returnedEvent,
                EventSeats = seats,
                EventAreas = areas
            };
            return View(returnedModel);
        }


        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditMainLayout(int? id)
        {
            if (!id.HasValue)
            {
                return View(new LayoutAreaViewModel());
            }

            var seats = _venueService.GetSeatsByLayout(id.Value).ToList();
            var areas = _venueService.GetAreasByLayout(id.Value).ToList();

            var returnedModel = new LayoutAreaViewModel
            {
                LayoutId = id.Value,
                Seats = seats,
                Areas = areas
            };
            return View(returnedModel);
        }
    }
}