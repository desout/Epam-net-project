using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using EpamNetProject.PLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.PLL.Managers
{
    public class MyUserStore : IUserStore<User, string>,
        IUserClaimStore<User, string>,
        IUserPasswordStore<User, string>
    {
        private readonly IUserService _userService;
        private readonly IClaimService _claimService;
        private readonly IUserMapperConfigurationProvider _userMapperConfigurationProvider;
        
        public MyUserStore(IUserService userService, IUserMapperConfigurationProvider userMapperConfigurationProvider, IClaimService claimService)
        {
            _userService = userService;
            _userMapperConfigurationProvider = userMapperConfigurationProvider;
            _claimService = claimService;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(User user)
        {
            return _userService.CreateUser(user);
        }

        public Task UpdateAsync(User user)
        {
            return _userService.UpdateUser(user);
        }

        public Task DeleteAsync(User user)
        {
           return _userService.DeleteUser(user);
        }

        public Task<User> FindByIdAsync(string userId)
        {
           return _userService.GetUser(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _userService.getUserByName(userName); 
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return user.Claims.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue)).ToList();
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            return _claimService.AddClaim(user, claim);
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            return _claimService.RemoveClaim(user, claim);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return _userService.GetPasswordHashAsync(user);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return _userService.HasPasswordAsync(user);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return _userService.SetPasswordHashAsync(user, passwordHash);
        }
    }
}
