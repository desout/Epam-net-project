using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

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

        [HttpGet]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult EditLayouts()
        {
            return View(_venueService.GetLayouts());
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
                    ImgUrl = eventDto.ImgUrl,
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
            try
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
            }
            catch (Exception)
            {
                // ignored
            }

            return View("Edit", model);
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
            return View(new LayoutViewModel
            {
                Id = layoutDto.Id,
                VenueId = layoutDto.VenueId,
                Description = layoutDto.Description,
                LayoutName = layoutDto.LayoutName
            });
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddLayout(LayoutViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != null)
                    {
                        _venueService.UpdateLayout(new LayoutDto
                        {
                            Id = model.Id.Value,
                            Description = model.Description,
                            LayoutName = model.LayoutName,
                            VenueId = model.VenueId
                        });
                    }
                    else
                    {
                        _venueService.CreateLayout(new LayoutDto
                        {
                            Description = model.Description,
                            LayoutName = model.LayoutName,
                            VenueId = model.VenueId
                        });
                    }

                    return RedirectToAction("EditLayouts");
                }
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

            var seats = _eventService.GetSeatsByEvent(id, User.Identity.GetUserId()).ToList();
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
        public JsonResult AddArea(AddAreaModel area)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            var baseArea = _eventService.CreateEventArea(new EventAreaDto
            {
                Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                CoordY = area.LeftCorner.Y, EventId = area.EventId
            });
            return new JsonResult
            {
                Data = new {Success = true, Area = baseArea},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public JsonResult ChangeArea(EventAreaDto area)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {
                _eventService.UpdateEventArea(area);
                return new JsonResult
                {
                    Data = new {Success = true},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public JsonResult RemoveSeat(int id)
        {
            var removeId = _eventService.RemoveSeat(id);
            return Json(removeId > 0);
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddSeat(AddSeatModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {
                var baseSeat = _eventService.AddSeat(new EventSeatDto
                {
                    Row = model.Row, Number = model.Number, EventAreaId = model.AreaId
                });
                return new JsonResult
                {
                    Data = new {Success = true, Seat = baseSeat},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            catch (EntityException e)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
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

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult ChangeMainArea(AreaDto area)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {
                _venueService.UpdateArea(area);
                return new JsonResult
                {
                    Data = new {Success = true},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

        [Authorize(Roles = "Manager, Admin")]
        public ActionResult AddMainSeat(AddSeatModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {
                var baseSeat = _venueService.AddSeat(new SeatDto
                {
                    Row = model.Row, Number = model.Number, AreaId = model.AreaId
                });
                return new JsonResult
                {
                    Data = new {Success = true, Seat = baseSeat},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            catch (EntityException e)
            {
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
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
                return new JsonResult
                {
                    Data = new {Success = false},
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            var baseArea = _venueService.CreateArea(new AreaDto
            {
                Description = area.Description, Price = area.Price, CoordX = area.LeftCorner.X,
                CoordY = area.LeftCorner.Y, LayoutId = area.EventId
            });
            return new JsonResult
            {
                Data = new {Success = true, Area = baseArea},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}