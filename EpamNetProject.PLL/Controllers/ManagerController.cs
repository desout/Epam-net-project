using System.Linq;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Controllers
{
    public class ManagerController : Controller
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
            ViewBag.Layouts = _venueService.GetLayouts().ToList();
            if (id != null)
            {
                var eventDto = _eventService.GetEvent(id.Value);
                return View(new EditEventViewModel
                {
                    Id = eventDto.Id,
                    Name = eventDto.Name,
                    Description = eventDto.Description,
                    Time = eventDto.EventDate,
                    Title = eventDto.Name,
                    ImgUrl = string.Empty,
                    Layout = eventDto.LayoutId
                });
            }

            return View(new EditEventViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public JsonResult DeleteEvent(int id)
        {
            var removeId = _eventService.RemoveEvent(id);
            return Json(removeId > 0);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Add(EditEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null)
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
                }
                else
                {
                    _eventService.CreateEvent(new EventDto
                    {
                        Description = model.Description,
                        EventDate = model.Time,
                        Name = model.Title,
                        LayoutId = model.Layout,
                        ImgUrl = model.ImgUrl
                    });
                }

                return RedirectToAction("EditEvents");
            }

            return View("Edit", model);
        }
    }
}
