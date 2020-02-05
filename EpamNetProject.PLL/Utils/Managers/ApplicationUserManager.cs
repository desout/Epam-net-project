using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class ApplicationUserManager : UserManager<UserDTO, string>
    {
        public ApplicationUserManager(IUserStore<UserDTO, string> store)
            : base(store)
        {
        }
    }
}