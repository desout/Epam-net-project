using System.Web.Mvc;

namespace EpamNetProject.PLL.Controllers
{
    public class ISMController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}
