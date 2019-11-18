using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.PLL.Infrastucture;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserProfileDTO getUserProfile(string userId);
        UserDTO getUserInfo(string userId);
        decimal addBalance(string userId, decimal amount);
        bool UpdateUserInfo(UserDTO user);
    }
}
