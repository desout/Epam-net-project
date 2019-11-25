using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class ApplicationUserManager : UserManager<User, string>
    {
        public ApplicationUserManager(IUserStore<User, string> store)
            : base(store)
        {
        }
    }
}
