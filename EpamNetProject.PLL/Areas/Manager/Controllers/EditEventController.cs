using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Helpers;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Models;

namespace EpamNetProject.PLL.Areas.Manager.Controllers
{
    public class EditEventController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IVenueService _venueService;

        private readonly IMapper _mapper;
        public EditEventController(IEventService eventService, IVenueService venueService, IUserMapperConfigurationProvider userMapperConfigurationProvider)
        {
            _eventService = eventService;
            _venueService = venueService;
            _mapper = userMapperConfigurationProvider.GetMapperConfig();
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditEvents()
        {
            return View(_eventService.GetAllEvents());
        }


        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Layouts = _venueService.GetLayouts().ToList();
            if (id == null)
            {
                return View(new EditEventViewModel());
            }

            var eventDto = _eventService.GetEvent(id.Value);
            return View(_mapper.Map<EditEventViewModel>(eventDto));
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult DeleteEvent(int id)
        {
            var removeId = _eventService.RemoveEvent(id);
            return removeId > 0 ? HttpResponseHelper.Ok() : HttpResponseHelper.Error();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Add(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                _eventService.CreateEvent(_mapper.Map<EventDto>(model));

                return RedirectToAction("EditEvents");
            }

            catch (Exception)
            {
                // ignored
            }

            return View("Edit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Update(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                _eventService.UpdateEvent(_mapper.Map<EventDto>(model));
                return RedirectToAction("EditEvents");
            }
            catch (Exception)
            {
                // ignored
            }

            return View("Edit", model);
        }
    }
}
