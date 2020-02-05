using EpamNetProject.DAL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class ApplicationUserRoleManager : RoleManager<UserRole, string>
    {
        public ApplicationUserRoleManager(IRoleStore<UserRole, string> store)
            : base(store)
        {
        }
    }
}