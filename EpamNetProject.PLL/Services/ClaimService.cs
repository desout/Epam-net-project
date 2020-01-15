using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.PLL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.PLL.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IAsyncRepository<IdentityUserClaim> _claimRepository;

        public ClaimService(IAsyncRepository<IdentityUserClaim> claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task RemoveClaim(UserDTO user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            IEnumerable<IdentityUserClaim> claims =
                user.Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList();
            foreach (var c in claims)
            {
                await _claimRepository.Delete(c);
            }

            Task.FromResult(0);
        }

        public Task AddClaim(UserDTO user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            _claimRepository.Create(new IdentityUserClaim
                {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value});
            return Task.FromResult(0);
        }
    }
}