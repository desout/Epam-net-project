using System;
using System.Configuration;
using System.Web;

namespace EpamNetProject.PLL.Extensions
{
    public static class DateTimeExtension
    {
        public static HttpCookie ToJsCookieTime(this DateTime basketTime)
        {
            var delay = int.Parse(ConfigurationManager.AppSettings["basketLeaveTime"]);
            var expires = delay - TimeSpan.FromTicks(DateTime.UtcNow.Ticks - basketTime.Ticks)
                              .TotalMinutes;
            var returnedValue = basketTime
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
            return new HttpCookie("basketTime")
            {
                HttpOnly = false, Value = returnedValue.ToString(), Expires = DateTime.Now.AddMinutes(expires)
            };
        }
    }
}