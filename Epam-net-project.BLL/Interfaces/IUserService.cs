using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto, string passwordHash);
        UserProfileDTO getUserProfile(string userId);
        UserDTO getUserInfo(string userId);
        decimal addBalance(string userId, decimal amount);
        bool UpdateUserInfo(UserDTO user, string passwordHash);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task<User> GetUser(string userId);
        Task<User> getUserByName(string userName);
        Task<string> GetPasswordHashAsync(User user);
        Task<bool> HasPasswordAsync(User user);
        Task SetPasswordHashAsync(User user, string passwordHash);
    }
}
