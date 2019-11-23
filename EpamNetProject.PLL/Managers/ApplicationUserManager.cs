using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }
    }
}
