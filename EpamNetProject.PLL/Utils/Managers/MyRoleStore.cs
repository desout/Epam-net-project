using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.Models;
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


        public async Task CreateAsync(UserRole role)
        {
            await _roleService.CreateRole(role);
        }

        public async Task UpdateAsync(UserRole role)
        {
            await _roleService.UpdateRole(role);
        }

        public async Task DeleteAsync(UserRole role)
        {
            await _roleService.DeleteRole(role);
        }

        public async Task<UserRole> FindByIdAsync(string roleId)
        {
            return await _roleService.GetRole(roleId);
        }

        public async Task<UserRole> FindByNameAsync(string roleName)
        {
            return await _roleService.FindByNameRole(roleName);
        }
    }
}