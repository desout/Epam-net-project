using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Managers;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Services
{
    public class MyUserService: IMyUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationRoleManager _applicationRoleManager;
        private readonly IUserService _userService;

        public MyUserService(IUserService userService, ApplicationRoleManager applicationRoleManager, ApplicationUserManager applicationUserManager)
        {
            _userService = userService;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            var user = await _applicationUserManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
                claim = await _applicationUserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _applicationRoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole {Name = roleName};
                    await _applicationRoleManager.CreateAsync(role);
                }
            }

            var hashPassword = _applicationUserManager.PasswordHasher.HashPassword(adminDto.Password);
            await _userService.Create(adminDto,hashPassword);
        }
    }
}
