using System.Threading.Tasks;
using EpamNetProject.DAL.models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(UserRole role);
        Task UpdateRole(UserRole role);
        Task DeleteRole(UserRole role);
        Task<UserRole> GetRole(string roleId);
        Task<UserRole> FindByNameRole(string roleName);
        
    }
}
