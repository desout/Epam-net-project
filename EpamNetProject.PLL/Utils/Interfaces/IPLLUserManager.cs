using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Utils.Interfaces
{
    public interface IPLLUserManager
    {
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        List<string> Register(UserDTO user);
    }
}
