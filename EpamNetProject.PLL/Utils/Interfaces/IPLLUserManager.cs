using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.PLL.Utils.Interfaces
{
    public interface IPllUserManager
    {
        Task<ClaimsIdentity> Authenticate(UserDto userDto);

        List<string> Register(UserDto user);
    }
}