using EpamNetProject.BLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Utils.Managers
{
    public class ApplicationUserManager : UserManager<UserDto, string>
    {
        public ApplicationUserManager(IUserStore<UserDto, string> store)
            : base(store)
        {
        }
    }
}