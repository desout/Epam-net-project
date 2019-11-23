using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.DAL.models;

namespace EpamNetProject.BLL.Interfaces
{
    public interface IClaimService
    {
        Task RemoveClaim(User user, Claim claim);
        Task AddClaim(User user, Claim claim);
    }
}
