using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class MyRoleStore : IRoleStore<UserRole>
    {
        private readonly IRoleService _roleService;

        public MyRoleStore(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public void Dispose()
        {
        }


        public Task CreateAsync(UserRole role)
        {
            return _roleService.CreateRole(role);
        }

        public Task UpdateAsync(UserRole role)
        {
            return _roleService.UpdateRole(role);
        }

        public Task DeleteAsync(UserRole role)
        {
            return _roleService.DeleteRole(role);
        }

        public Task<UserRole> FindByIdAsync(string roleId)
        {
            return _roleService.GetRole(roleId);
        }

        public Task<UserRole> FindByNameAsync(string roleName)
        {
            return _roleService.FindByNameRole(roleName);
        }
    }
}