using System;
using System.Linq;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Helpers;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Areas.Manager.Controllers
{
    public class EditEventController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IVenueService _venueService;

        public EditEventController(IEventService eventService, IVenueService venueService)
        {
            _eventService = eventService;
            _venueService = venueService;
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
            return View(new EditEventViewModel
            {
                Id = eventDto.Id,
                Name = eventDto.Name,
                Description = eventDto.Description,
                Time = eventDto.EventDate,
                Title = eventDto.Name,
                ImgUrl = eventDto.ImgUrl,
                Layout = eventDto.LayoutId
            });
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
                _eventService.CreateEvent(new EventDto
                {
                    Description = model.Description,
                    EventDate = model.Time,
                    Name = model.Title,
                    LayoutId = model.Layout,
                    ImgUrl = model.ImgUrl
                });

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
                _eventService.UpdateEvent(new EventDto
                {
                    Id = model.Id.Value,
                    Description = model.Description,
                    EventDate = model.Time,
                    Name = model.Title,
                    LayoutId = model.Layout,
                    ImgUrl = model.ImgUrl
                });
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