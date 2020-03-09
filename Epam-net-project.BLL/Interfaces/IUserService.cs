using System.Threading.Tasks;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserDto userDto, string passwordHash);

        UserProfileDto GetUserProfile(string userId);

        UserDto GetUserInfo(string userId);

        decimal AddBalance(string userId, decimal amount);

        bool UpdateUserInfo(UserDto user, string passwordHash);

        Task CreateUser(UserDto user);

        Task UpdateUser(UserDto user);

        Task DeleteUser(UserDto user);

        Task<UserDto> GetUser(string userId);

        Task<UserDto> GetUserByName(string userName);

        Task<string> GetPasswordHashAsync(UserDto user);

        Task<bool> HasPasswordAsync(UserDto user);

        Task SetPasswordHashAsync(UserDto user, string passwordHash);

        void AddUserProfile(UserDto userDto, UserProfileDto userProfile);
    }
}