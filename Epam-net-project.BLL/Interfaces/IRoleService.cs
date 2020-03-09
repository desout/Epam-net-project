using System.Collections.Generic;
using System.Threading.Tasks;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(UserRole role);

        Task UpdateRole(UserRole role);

        Task DeleteRole(UserRole role);

        Task<UserRole> GetRole(string roleId);

        Task<UserRole> FindByNameRole(string roleName);

        Task AddToRole(UserDto user, string roleName);

        Task RemoveFromRole(UserDto user, string roleName);

        Task<IList<string>> GetRoles(UserDto user);

        Task<bool> IsInRole(UserDto user, string roleName);
    }
}