using System.Collections.Generic;
using System.Threading.Tasks;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(UserRole role);

        Task UpdateRole(UserRole role);

        Task DeleteRole(UserRole role);

        Task<UserRole> GetRole(string roleId);

        Task<UserRole> FindByNameRole(string roleName);

        Task AddToRole(UserDTO user, string roleName);

        Task RemoveFromRole(UserDTO user, string roleName);

        Task<IList<string>> GetRoles(UserDTO user);

        Task<bool> IsInRole(UserDTO user, string roleName);
    }
}