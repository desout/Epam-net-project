using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Utils.Extensions
{
    public static class PrincipalExtenstion
    {
        public static string GetUserId(this IPrincipal user)
        {
            return user.Identity.GetUserId();
        }
    }
}