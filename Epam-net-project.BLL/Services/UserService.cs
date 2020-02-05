using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EpamNetProject.BLL.Infrastucture;
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

        public async Task<OperationDetails> Create(UserDTO userDto, string passwordHash)
        {
            var user = (await _userRepository.GetAll()).FirstOrDefault(x => x.UserName == userDto.UserName);
            if (user != null)
            {
                return new OperationDetails(false, "User with this name exist", "Email");
            }

            user = new User {Email = userDto.Email, UserName = userDto.UserName, PasswordHash = passwordHash};
            await _userRepository.Create(user);
            userDto.UserProfile.UserId = user.Id;
            _userProfileRepository.Add(_mapper.Map<UserProfile>(userDto.UserProfile));
            return new OperationDetails(true, "Register successful", "");
        }

        public UserProfileDTO getUserProfile(string userId)
        {
            return _mapper.Map<UserProfileDTO>(_userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId));
        }


        public UserDTO getUserInfo(string userId)
        {
            var user = _userRepository.GetAll().Result.FirstOrDefault(x => x.Id == userId);
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                return new UserDTO();
            }

            var returnedUser = new UserDTO
            {
                Email = user.Email,
                UserProfile = _mapper.Map<UserProfileDTO>(userProfile)
            };
            return returnedUser;
        }

        public bool UpdateUserInfo(UserDTO user, string passwordHash)
        {
            var localUser = _userRepository.GetAll().Result.FirstOrDefault(x => x.Id == user.Id);
            if (passwordHash != "" || user.Email != localUser?.Email)
            {
                if (passwordHash != "")
                {
                    localUser.PasswordHash = passwordHash;
                }

                localUser.Email = user.Email;
                _userRepository.Update(localUser);
            }

            var profile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == user.Id);
            profile.FirstName = user.UserProfile.FirstName;
            profile.Language = user.UserProfile.Language;
            profile.Surname = user.UserProfile.Surname;
            profile.TimeZone = user.UserProfile.TimeZone;
            _userProfileRepository.Update(profile);

            return true;
        }

        public Task CreateUser(UserDTO user)
        {
            return _userRepository.Create(_mapper.Map<User>(user));
        }

        public Task UpdateUser(UserDTO user)
        {
            return _userRepository.Update(_mapper.Map<User>(user));
        }

        public Task DeleteUser(UserDTO user)
        {
            return _userRepository.Delete(_mapper.Map<User>(user));
        }

        public async Task<UserDTO> GetUser(string userId)
        {
            return _mapper.Map<UserDTO>(await _userRepository.Get(userId));
        }

        public async Task<UserDTO> getUserByName(string userName)
        {
            return _mapper.Map<UserDTO>((await _userRepository.GetAll()).FirstOrDefault(x => x.UserName == userName));
        }

        public async Task<string> GetPasswordHashAsync(UserDTO user)
        {
            return (await _userRepository.Get(user.Id)).PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(UserDTO user)
        {
            return string.IsNullOrEmpty((await _userRepository.Get(user.Id)).PasswordHash);
        }

        public Task SetPasswordHashAsync(UserDTO user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return _userRepository.Update(_mapper.Map<User>(user));
        }

        public void AddUserProfile(UserDTO userDto, UserProfileDTO userProfile)
        {
            userProfile.UserId = userDto.Id;
            _userProfileRepository.Add(_mapper.Map<UserProfile>(userProfile));
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