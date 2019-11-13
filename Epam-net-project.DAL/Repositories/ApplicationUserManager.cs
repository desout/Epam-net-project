using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.DAL.Repositories
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }
    }
}
