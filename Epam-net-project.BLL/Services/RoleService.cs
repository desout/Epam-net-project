using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.BLL.Services
{
    public class RoleService : IRoleService
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
            return Task.FromResult(_roleRepository.GetAll().Result.FirstOrDefault(x => x.Name == roleName));
        }

        public async Task AddToRole(User user, string roleName)
        {
            var role = (await _roleRepository.GetAll()).FirstOrDefault(x => x.Name == roleName);
            if (role != null)
            {
                role.Users.Add(new IdentityUserRole {RoleId = role.Id, UserId = user.Id});
                await _roleRepository.Update(role);
            }
        }

        public async Task RemoveFromRole(User user, string roleName)
        {
            var role = (await _roleRepository.GetAll()).FirstOrDefault(x => x.Name == roleName);
            if (role != null)
            {
                role.Users.Remove(new IdentityUserRole {RoleId = role.Id, UserId = user.Id});
                await _roleRepository.Update(role);
            }
        }

        public async Task<IList<string>> GetRoles(User user)
        {
            var roles = await _roleRepository.GetAll();
            return roles.Where(x => x.Users.Contains(new IdentityUserRole {RoleId = x.Id, UserId = user.Id}))
                .Select(x => x.Name).ToList();
        }

        public async Task<bool> IsInRole(User user, string roleName)
        {
            var role = (await _roleRepository.GetAll()).FirstOrDefault(x => x.Name == roleName);
            return role != null && role.Users.Contains(new IdentityUserRole {RoleId = role.Id, UserId = user.Id});
        }
    }
}