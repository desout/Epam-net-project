using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Utils.Interfaces;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Utils.Managers
{
    public class MyUserStore : IUserStore<UserDto, string>,
        IUserClaimStore<UserDto, string>,
        IUserPasswordStore<UserDto, string>,
        IUserRoleStore<UserDto, string>
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

        public async Task<IList<Claim>> GetClaimsAsync(UserDto user)
        {
            return user.Claims.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(UserDto user, Claim claim)
        {
            await _claimService.AddClaim(user, claim);
        }

        public async Task RemoveClaimAsync(UserDto user, Claim claim)
        {
            await _claimService.RemoveClaim(user, claim);
        }

        public async Task<string> GetPasswordHashAsync(UserDto user)
        {
            return await _userService.GetPasswordHashAsync(user);
        }

        public async Task<bool> HasPasswordAsync(UserDto user)
        {
            return await _userService.HasPasswordAsync(user);
        }

        public async Task SetPasswordHashAsync(UserDto user, string passwordHash)
        {
            await _userService.SetPasswordHashAsync(user, passwordHash);
        }

        public async Task AddToRoleAsync(UserDto user, string roleName)
        {
            await AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));
        }

        public async Task RemoveFromRoleAsync(UserDto user, string roleName)
        {
            await _roleService.RemoveFromRole(user, roleName);
        }

        public async Task<IList<string>> GetRolesAsync(UserDto user)
        {
            return await _roleService.GetRoles(user);
        }

        public async Task<bool> IsInRoleAsync(UserDto user, string roleName)
        {
            return await _roleService.IsInRole(user, roleName);
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(UserDto user)
        {
            await _userService.CreateUser(user);
        }

        public async Task UpdateAsync(UserDto user)
        {
            await _userService.UpdateUser(user);
        }

        public async Task DeleteAsync(UserDto user)
        {
            await _userService.DeleteUser(user);
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            return await _userService.GetUser(userId);
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            return await _userService.GetUserByName(userName);
        }
    }
}