using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.DAL.Repositories
{
    public class ApplicationRoleManager : RoleManager<UserRole>
    {
        public ApplicationRoleManager(RoleStore<UserRole> store)
            : base(store)
        { }
    }
}
