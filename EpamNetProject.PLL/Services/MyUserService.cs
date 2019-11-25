using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Interfaces;
using EpamNetProject.PLL.Managers;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Services
{
    public class MyUserService : IMyUserService
    {
        private readonly ApplicationUserRoleManager _applicationRoleManager;

        private readonly ApplicationUserManager _applicationUserManager;

        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public MyUserService(IUserService userService, ApplicationUserRoleManager applicationRoleManager,
            ApplicationUserManager applicationUserManager,
            IUserMapperConfigurationProvider userMapperConfigurationProvider)
        {
            _userService = userService;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _mapper = userMapperConfigurationProvider.GetMapperConfig();
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            var user = await _applicationUserManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
            {
                claim = await _applicationUserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            }

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
            await _userService.Create(adminDto, hashPassword);
            adminDto.Id = _userService.getUserByName(adminDto.UserName).Result.Id;
            _applicationUserManager.AddToRole(adminDto.Id, adminDto.Role);
        }

        public List<string> Register(UserDTO user)
        {
            var hashPassword = _applicationUserManager.PasswordHasher.HashPassword(user.Password);
            var operationDetails = _applicationUserManager.Create(_mapper.Map<User>(user), hashPassword);
            if (operationDetails.Errors.Any())
            {
                return operationDetails.Errors.ToList();
            }

            user.Id = _userService.getUserByName(user.UserName).Result.Id;
            _applicationUserManager.AddToRole(user.Id, user.Role);
            _userService.AddUserProfile(user, user.UserProfile);
            return new List<string>();
        }
    }
}