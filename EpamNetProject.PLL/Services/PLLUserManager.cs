using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Models;
using EpamNetProject.PLL.Utils.Interfaces;
using EpamNetProject.PLL.Utils.Managers;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.PLL.Services
{
    public class PLLUserManager : IPLLUserManager
    {
        private readonly ApplicationUserRoleManager _applicationRoleManager;

        private readonly ApplicationUserManager _applicationUserManager;

        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public PLLUserManager(IUserService userService, ApplicationUserRoleManager applicationRoleManager,
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

        public List<string> Register(UserDTO user)
        {
            try
            {
                _applicationUserManager.Create(user, user.Password);
                user.Id = _userService.GetUserByName(user.UserName).Result.Id;
                _applicationUserManager.AddToRole(user.Id, user.Role);
                _userService.AddUserProfile(user, user.UserProfile);
                return new List<string>();
            }
            catch (EntityException e)
            {
                return e.Message.Select(x=>x.ToString()).ToList();
            }

        }
    }
}
