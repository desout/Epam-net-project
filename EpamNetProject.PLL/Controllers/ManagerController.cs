using System;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Controllers
{
    public class ManagerController: Controller
    {
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;
        public ManagerController(IEventService eventService, IVenueService venueService)
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
            if (id != null)
            {
                var eventDto = _eventService.GetEvent(id.Value);
                return View(new EditEventViewModel
                {
                    Id = eventDto.Id,
                    Description = eventDto.Description,
                    Time = eventDto.EventDate,
                    Title = eventDto.Name,
                    imgUrl = String.Empty,
                    Layout = eventDto.LayoutId
                });
            }

            ViewBag.Layouts = _venueService.GetLayouts();
            return View(new EditEventViewModel());
        }
        
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public JsonResult DeleteEvent(int id)
        {
           var removeId = _eventService.RemoveEvent(id);
           return Json(removeId > 0);
        }
        
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Add(EditEventViewModel model)
        {
            if (model.Id != null)
            {
                _eventService.UpdateEvent(new EventDto
                {
                    Id = model.Id.Value,
                    Description = model.Description,
                    EventDate = model.Time,
                    Name = model.Title,
                    LayoutId = model.Layout

                });
            }
            else
            {
                _eventService.CreateEvent(new EventDto
                {
                    Description = model.Description,
                    EventDate = model.Time,
                    Name = model.Title,
                    LayoutId = model.Layout

                });
            }

            return RedirectToAction("EditEvents");
        }
    }
    
}
