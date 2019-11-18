using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EpamNetProject.PLL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IEventService eventService)
        {
            _userService = userService;
            _eventService = eventService;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        [Authorize]
        public ActionResult Profile()
        {
            return View();
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
            _userService.UpdateUserInfo(new UserDTO
            {
                Id = model.UserId,
                Email = model.Email,
                Password = model.Password,
                UserProfile = new UserProfileDTO
                {
                    FirstName = model.FirstName,
                    Language = model.Language,
                    Surname = model.Surname,
                    TimeZone = model.TimeZone
                }
            });
            return View("SaveProfileSuccess");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PurchaseHistory()
        {
            var items = _eventService.GetUserPurchaseHistory(AuthenticationManager.User.Identity.GetUserId());
            return View(items);
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
                var claim = await _userService.Authenticate(userDto);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
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
                var operationDetails = await _userService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddToBalance(decimal count)
        {
            var newBalance = _userService.addBalance(User.Identity.GetUserName(), count);
            return new JsonResult
            {
                Data = new {Success = true, NewBalance = newBalance},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
            ;
        }
    }
}