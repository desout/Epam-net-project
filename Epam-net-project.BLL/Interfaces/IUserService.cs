using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Infrastucture;
using EpamNetProject.BLL.models;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
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
