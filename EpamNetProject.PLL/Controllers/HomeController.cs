using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IPLLUserManager userManager)
        {
            userManager.SetInitialData(new List<UserDTO>
            {
                new UserDTO
                {
                    Email = "3809766@mail.ru",
                    UserName = "desout",
                    Password = "Desoutside1",
                    Role = "Admin",
                    UserProfile = new UserProfileDTO
                    {
                        FirstName = "Andrei", Surname = "Anelkin", Language = "en", TimeZone = "UTC-11",
                        Balance = 100000
                    }
                },
                new UserDTO
                {
                    Email = "1015036@mail.ru",
                    UserName = "desout1",
                    Password = "Desoutside1",
                    Role = "User",
                    UserProfile = new UserProfileDTO
                        {FirstName = "Andrei1", Surname = "Anelkin1", Language = "en", TimeZone = "UTC-11", Balance = 0}
                }
            }, new List<string> {"User", "Admin", "Manager"});
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult ChangeCulture(string newCulture)
        {
            var returnUrl = Request.UrlReferrer?.AbsolutePath;
            var cultures = new List<string> {"ru", "en", "blr"};
            if (!cultures.Contains(newCulture))
            {
                newCulture = "en";
            }

            var cookie = new HttpCookie("lang")
                {HttpOnly = false, Value = newCulture, Expires = DateTime.Now.AddYears(1)};
            Response.Cookies.Set(cookie);
            return Redirect(returnUrl);
        }
    }
}
