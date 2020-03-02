using EpamNetProject.BLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Utils.Managers
{
    public class ApplicationUserManager : UserManager<UserDTO, string>
    {
        public ApplicationUserManager(IUserStore<UserDTO, string> store)
            : base(store)
        {
        }
    }
}