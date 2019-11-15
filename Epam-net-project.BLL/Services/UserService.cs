using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.models;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using Microsoft.AspNet.Identity;

namespace EpamNetProject.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationRoleManager _applicationRoleManager;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IMapper _mapper;

        public UserService(ApplicationUserManager applicationUserManager, ApplicationRoleManager applicationRoleManager,
            IRepository<UserProfile> userProfileRepository)
        {
            _applicationRoleManager = applicationRoleManager;
            _userProfileRepository = userProfileRepository;
            _applicationUserManager = applicationUserManager;
            _mapper = MapperConfigurationProvider.GetMapperConfig();
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await _applicationUserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User {Email = userDto.Email, UserName = userDto.UserName};
                var result = await _applicationUserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _applicationUserManager.AddToRoleAsync(user.Id, userDto.Role);
                userDto.UserProfile.UserId = user.Id;
                _userProfileRepository.Add(_mapper.Map<UserProfile>(userDto.UserProfile));
                //await _applicationUserManager.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public UserProfileDTO getUserProfile(string userId)
        {
            return _mapper.Map<UserProfileDTO>(_userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId));
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await _applicationUserManager.FindAsync(userDto.UserName, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                claim = await _applicationUserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _applicationRoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole() {Name = roleName};
                    await _applicationRoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        public UserDTO getUserInfo(string userId)
        {
            var user = _applicationUserManager.Users.FirstOrDefault(x => x.Id == userId);
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            if (user == null) return new UserDTO();
            var returnedUser = new UserDTO
            {
                Email = user.Email,
                UserProfile = _mapper.Map<UserProfileDTO>(userProfile)
            };
            return returnedUser;
        }

        public bool UpdateUserInfo(UserDTO user)
        {
            var localUser = _applicationUserManager.Users.FirstOrDefault(x => x.Id == user.Id);
            if (user.Password != "" || user.Email != localUser.Email)
            {
                if (user.Password != "")
                {
                    localUser.PasswordHash = _applicationUserManager.PasswordHasher.HashPassword(user.Password);
                }

                localUser.Email = user.Email;
                _applicationUserManager.Update(localUser);
            }

            _userProfileRepository.Update(_mapper.Map<UserProfile>(user.UserProfile));
            
            return true;
        }

        public decimal addBalance(string userId, decimal amount)
        {
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            userProfile.Balance += amount;
            _userProfileRepository.Update(userProfile);
            return userProfile.Balance;
        }
    }
}
