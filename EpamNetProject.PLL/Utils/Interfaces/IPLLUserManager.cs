using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Interfaces
{
    public interface IPLLUserManager
    {
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        Task SetInitialData(List<UserDTO> adminDto, List<string> roles);

        List<string> Register(UserDTO user);
    }
}