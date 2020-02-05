using System.Web.Mvc;

namespace EpamNetProject.PLL.Helpers
{
    public static class HttpResponseHelper
    {
        public static JsonResult Ok(object data)
        {
            return new JsonResult
            {
                Data = new {Success = true, data},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public static JsonResult Error(object data)
        {
            return new JsonResult
            {
                Data = new {Success = false, data},
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
            ;
        }

        public static JsonResult Ok()
        {
            return Ok(new { });
        }

        public static JsonResult Error()
        {
            return Error(new { });
        }
    }
}