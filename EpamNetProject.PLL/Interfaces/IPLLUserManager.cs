using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.PLL.Interfaces
{
    public interface IPLLUserManager
    {
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        Task SetInitialData(UserDTO adminDto, List<string> roles);

        List<string> Register(UserDTO user);
    }
}