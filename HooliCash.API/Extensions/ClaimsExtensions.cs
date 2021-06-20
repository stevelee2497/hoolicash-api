using HooliCash.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HooliCash.API.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this IEnumerable<Claim> claims)
            => Guid.TryParse(GetClaimValue(claims, JwtClaimType.UserId), out Guid id)
                ? id
                : throw new AuthenticationException();

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.GetUserId();


        private static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            var claim = claims.FirstOrDefault(x => x.Type == claimType);
            if (claim == null) throw new AuthenticationException();
            return claim.Value;
        }
    }
}
