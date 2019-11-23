using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class ApplicationRoleManager : RoleManager<UserRole, string>
    {
        public ApplicationRoleManager(IRoleStore<UserRole, string> store)
            : base(store)
        {
        }
    }
}
