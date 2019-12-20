using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.DAL.models;
using EpamNetProject.PLL.Models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IClaimService
    {
        Task RemoveClaim(UserDTO user, Claim claim);

        Task AddClaim(UserDTO user, Claim claim);
    }
}
