using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationRoleManager _applicationRoleManager;

        public UserService(ApplicationUserManager applicationUserManager, ApplicationRoleManager applicationRoleManager)
        {
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
        }
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await _applicationUserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.UserName };
                var result = await _applicationUserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                    
                }
                await _applicationUserManager.AddToRoleAsync(user.Id, userDto.Role);
                //await _applicationUserManager.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await _applicationUserManager.FindAsync(userDto.UserName, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if(user!=null)
                claim= await _applicationUserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _applicationRoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole() { Name = roleName };
                    await _applicationRoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
    }
}
