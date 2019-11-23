using System.Linq;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.BLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IAsyncRepository<UserRole> _roleRepository;

        public RoleService(IAsyncRepository<UserRole> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public Task CreateRole(UserRole role)
        {
            return _roleRepository.Create(role);
        }

        public Task UpdateRole(UserRole role)
        {
            return _roleRepository.Update(role);
        }

        public Task DeleteRole(UserRole role)
        {
            return _roleRepository.Create(role);
        }

        public Task<UserRole> GetRole(string roleId)
        {
            return _roleRepository.Get(roleId);
        }

        public Task<UserRole> FindByNameRole(string roleName)
        {
            return Task.FromResult(_roleRepository.GetAll().Result.FirstOrDefault(x=>x.Name == roleName));
        }
    }
}
