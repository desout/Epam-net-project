using System.Threading.Tasks;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDto, string passwordHash);

        UserProfileDTO GetUserProfile(string userId);

        UserDTO GetUserInfo(string userId);

        decimal AddBalance(string userId, decimal amount);

        bool UpdateUserInfo(UserDTO user, string passwordHash);

        Task CreateUser(UserDTO user);

        Task UpdateUser(UserDTO user);

        Task DeleteUser(UserDTO user);

        Task<UserDTO> GetUser(string userId);

        Task<UserDTO> GetUserByName(string userName);

        Task<string> GetPasswordHashAsync(UserDTO user);

        Task<bool> HasPasswordAsync(UserDTO user);

        Task SetPasswordHashAsync(UserDTO user, string passwordHash);

        void AddUserProfile(UserDTO userDto, UserProfileDTO userProfile);
    }
}