using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IClaimService
    {
        Task RemoveClaim(UserDto user, Claim claim);

        Task AddClaim(UserDto user, Claim claim);
    }
}