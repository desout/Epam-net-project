using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Managers;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EpamNetProject.PLL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IMyUserService _myUserService;

        private readonly ApplicationUserManager _userManager;

        private readonly IUserService _userService;

        public AccountController(IEventService eventService, ApplicationUserManager userManager,
            IUserService userService, IMyUserService myUserService)
        {
            _userService = userService;
            _myUserService = myUserService;
            _eventService = eventService;
            _userManager = userManager;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        [Authorize]
        public ActionResult Profile()
        {
            var userProfile = _userService.getUserProfile(User.Identity.GetUserId());
            return View(userProfile);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            var user = _userService.getUserInfo(User.Identity.GetUserId());
            return View(new UserProfileViewModel
            {
                Email = user.Email,
                FirstName = user.UserProfile.FirstName,
                Surname = user.UserProfile.Surname,
                TimeZone = user.UserProfile.TimeZone,
                Language = user.UserProfile.Language,
                UserId = user.UserProfile.UserId
            });
        }

        [Authorize]
        public ActionResult SaveProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashPassword = string.IsNullOrEmpty(model.Password)
                    ? ""
                    : _userManager.PasswordHasher.HashPassword(model.Password);
                _userService.UpdateUserInfo(new UserDTO
                {
                    Id = model.UserId,
                    Email = model.Email,
                    UserProfile = new UserProfileDTO
                    {
                        FirstName = model.FirstName,
                        Language = model.Language,
                        Surname = model.Surname,
                        TimeZone = model.TimeZone
                    }
                }, hashPassword);
                var user = _userService.getUserInfo(User.Identity.GetUserId());
                var cookie = new HttpCookie("lang")
                    {HttpOnly = false, Value = user.UserProfile.Language, Expires = DateTime.Now.AddYears(1)};
                Response.Cookies.Set(cookie);
                return View("SaveProfileSuccess");
            }

            return View("Edit", model);
        }

        [HttpGet]
        [Authorize]
        public PartialViewResult PurchaseHistory()
        {
            var items = _eventService.GetUserPurchaseHistory(AuthenticationManager.User.Identity.GetUserId());
            return PartialView(items);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO {UserName = model.UserName, Password = model.Password};
                var claim = await _myUserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    var user = _userService.getUserInfo(User.Identity.GetUserId());
                    var cookie = new HttpCookie("lang")
                        {HttpOnly = false, Value = user.UserProfile.Language, Expires = DateTime.Now.AddYears(1)};
                    Response.Cookies.Set(cookie);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                ViewBag.Errors = new List<string>();
                if (ModelState.IsValid)
                {
                    var userDto = new UserDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                        UserName = model.UserName,
                        Role = "user",
                        UserProfile = new UserProfileDTO
                        {
                            Balance = 0,
                            FirstName = model.FirstName,
                            Language = model.Language,
                            ReserveDate = null,
                            Surname = model.Surname,
                            TimeZone = model.TimeZone
                        }
                    };

                    var errors = _myUserService.Register(userDto);
                    if (!errors.Any())
                    {
                        return View("SuccessRegister");
                    }

                    ViewBag.Errors = errors;
                }
            }
            catch (Exception)
            {
                // ignored
            }



            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddToBalance(decimal count)
        {
            var newBalance = _userService.addBalance(User.Identity.GetUserId(), count);
            return new JsonResult
            {
                Data = new {Success = true, NewBalance = newBalance},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}
