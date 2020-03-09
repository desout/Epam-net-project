using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.BLL.Models;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IRepository<UserProfile> _userProfileRepository;

        private readonly IAsyncRepository<User> _userRepository;

        public UserService(
            IRepository<UserProfile> userProfileRepository, IMapperConfigurationProvider mapperConfigurationProvider,
            IAsyncRepository<User> userRepository)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapperConfigurationProvider.GetMapperConfig();
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(UserDto userDto, string passwordHash)
        {
            if (await _userRepository.GetAll().AnyAsync(x => x.UserName == userDto.UserName))
            {
                throw new EntityException();
            }

            var user = new User {Email = userDto.Email, UserName = userDto.UserName, PasswordHash = passwordHash};
            await _userRepository.Create(user);
            userDto.UserProfile.UserId = user.Id;
            _userProfileRepository.Add(_mapper.Map<UserProfile>(userDto.UserProfile));
            return userDto;
        }

        public UserProfileDto GetUserProfile(string userId)
        {
            return _mapper.Map<UserProfileDto>(_userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId));
        }


        public UserDto GetUserInfo(string userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return new UserDto();
            }
            
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            var returnedUser = new UserDto
            {
                Email = user.Email,
                UserProfile = _mapper.Map<UserProfileDto>(userProfile)
            };
            
            return returnedUser;
        }

        public bool UpdateUserInfo(UserDto user, string passwordHash)
        {
            var localUser = _userRepository.GetAll().FirstOrDefault(x => x.Id == user.Id);
            if (passwordHash != "" || user.Email != localUser?.Email)
            {
                if (passwordHash != "")
                {
                    localUser.PasswordHash = passwordHash;
                }

                localUser.Email = user.Email;
                _userRepository.Update(localUser);
            } //

            var profile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == user.Id);
            profile.FirstName = user.UserProfile.FirstName;
            profile.Language = user.UserProfile.Language;
            profile.Surname = user.UserProfile.Surname;
            profile.TimeZone = user.UserProfile.TimeZone;
            _userProfileRepository.Update(profile);

            return true;
        }

        public Task CreateUser(UserDto user)
        {
            return _userRepository.Create(_mapper.Map<User>(user));
        }

        public Task UpdateUser(UserDto user)
        {
            return _userRepository.Update(_mapper.Map<User>(user));
        }

        public Task DeleteUser(UserDto user)
        {
            return _userRepository.Delete(_mapper.Map<User>(user));
        }

        public async Task<UserDto> GetUser(string userId)
        {
            return _mapper.Map<UserDto>(await _userRepository.Get(userId));
        }

        public async Task<UserDto> GetUserByName(string userName)
        {
            return _mapper.Map<UserDto>(_userRepository.GetAll().FirstOrDefault(x => x.UserName == userName));
        }

        public async Task<string> GetPasswordHashAsync(UserDto user)
        {
            return (await _userRepository.Get(user.Id)).PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(UserDto user)
        {
            return string.IsNullOrEmpty((await _userRepository.Get(user.Id)).PasswordHash);
        }

        public Task SetPasswordHashAsync(UserDto user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return _userRepository.Update(_mapper.Map<User>(user));
        }

        public void AddUserProfile(UserDto userDto, UserProfileDto userProfile)
        {
            userProfile.UserId = userDto.Id;
            _userProfileRepository.Add(_mapper.Map<UserProfile>(userProfile));
        }

        public decimal AddBalance(string userId, decimal amount)
        {
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            userProfile.Balance += amount;
            _userProfileRepository.Update(userProfile);
            return userProfile.Balance;
        }
    }
}
