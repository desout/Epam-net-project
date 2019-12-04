using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Managers
{
    public class MyUserStore : IUserStore<User, string>,
        IUserClaimStore<User, string>,
        IUserPasswordStore<User, string>,
        IUserRoleStore<User, string>
    {
        private readonly IClaimService _claimService;

        private readonly IRoleService _roleService;

        private readonly IUserMapperConfigurationProvider _userMapperConfigurationProvider;

        private readonly IUserService _userService;

        public MyUserStore(IUserService userService, IUserMapperConfigurationProvider userMapperConfigurationProvider,
            IClaimService claimService, IRoleService roleService)
        {
            _userService = userService;
            _userMapperConfigurationProvider = userMapperConfigurationProvider;
            _claimService = claimService;
            _roleService = roleService;
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return user.Claims.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(User user, Claim claim)
        {
            await _claimService.AddClaim(user, claim);
        }

        public async Task RemoveClaimAsync(User user, Claim claim)
        {
            await _claimService.RemoveClaim(user, claim);
        }

        public async Task<string> GetPasswordHashAsync(User user)
        {
           return await _userService.GetPasswordHashAsync(user);
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            return await _userService.HasPasswordAsync(user);
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash)
        {
            await _userService.SetPasswordHashAsync(user, passwordHash);
        }

        public async Task AddToRoleAsync(User user, string roleName)
        {
            await AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));
        }

        public async Task RemoveFromRoleAsync(User user, string roleName)
        {
            await _roleService.RemoveFromRole(user, roleName);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _roleService.GetRoles(user);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return await _roleService.IsInRole(user, roleName);
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(User user)
        {
            await _userService.CreateUser(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userService.UpdateUser(user);
        }

        public async Task DeleteAsync(User user)
        {
            await _userService.DeleteUser(user);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userService.GetUser(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userService.getUserByName(userName);
        }
    }
}
