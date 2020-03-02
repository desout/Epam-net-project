using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Extensions;
using EpamNetProject.PLL.Utils.Helpers;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Managers;
using EpamNetProject.PLL.Utils.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;

namespace EpamNetProject.PLL.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IPLLUserManager _ipllUserManager;

        private readonly IMapper _mapper;

        private readonly ApplicationUserManager _userManager;

        private readonly IUserService _userService;


        public AccountController(IEventService eventService, ApplicationUserManager userManager,
            IUserService userService, IPLLUserManager ipllUserManager,
            IUserMapperConfigurationProvider userMapperConfigurationProvider)
        {
            _userService = userService;
            _ipllUserManager = ipllUserManager;
            _eventService = eventService;
            _userManager = userManager;
            _mapper = userMapperConfigurationProvider.GetMapperConfig();
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        [Authorize]
        public ActionResult Profile()
        {
            var userProfile = _userService.GetUserProfile(User.GetUserId());
            return View(userProfile);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            var user = _userService.GetUserInfo(User.GetUserId());
            return View(_mapper.Map<UserProfileViewModel>(user));
        }

        [Authorize]
        public ActionResult SaveProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var hashPassword = string.IsNullOrEmpty(model.Password)
                ? ""
                : _userManager.PasswordHasher.HashPassword(model.Password);
            _userService.UpdateUserInfo(_mapper.Map<UserDTO>(model), hashPassword);
            var user = _userService.GetUserProfile(User.GetUserId());
            var cookie = new HttpCookie("lang")
                {HttpOnly = false, Value = user.Language, Expires = DateTime.Now.AddYears(1)};
            Response.Cookies.Set(cookie);
            return View("SaveProfileSuccess");

        }

        [HttpGet]
        [Authorize]
        public PartialViewResult PurchaseHistory()
        {
            var items = _eventService.GetUserPurchaseHistory(AuthenticationManager.User.GetUserId());
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userDto = new UserDTO {UserName = model.UserName, Password = model.Password};
            var claim = await _ipllUserManager.Authenticate(userDto);
            if (claim == null)
            {
                ModelState.AddModelError(string.Empty, Resource.ACCOUNT_ERRORINCORRECTLOGINPASSWORD);
            }
            else
            {
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                var user = _userService.GetUserProfile(AuthenticationManager.AuthenticationResponseGrant.Identity
                    .GetUserId());
                var cookie = new HttpCookie("lang")
                    {HttpOnly = false, Value = user.Language, Expires = DateTime.Now.AddYears(1)};
                Response.Cookies.Set(cookie);
                return RedirectToAction("Index", "Home", new {area = ""});
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Response.Cookies["basketTime"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home", new {area = ""});
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
                    var userDto = _mapper.Map<UserDTO>(model);

                    var errors = _ipllUserManager.Register(userDto);
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
        public ActionResult AddToBalance(decimal count)
        {
            var newBalance = _userService.AddBalance(User.GetUserId(), count);
            return HttpResponseHelper.Ok(new {NewBalance = newBalance});
        }

        public PartialViewResult Basket()
        {
            var countOfItems = 0;
            if (User.Identity.IsAuthenticated)
            {
                countOfItems = _eventService.GetReservedSeatByUser(User.GetUserId()).Count;
            }

            return PartialView("Basket", countOfItems);
        }
    }
}
