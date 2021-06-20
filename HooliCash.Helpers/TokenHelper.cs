using HooliCash.Core.Models;
using HooliCash.IHelpers;
using HooliCash.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HooliCash.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Jwt.Secret));

        public string GenerateEncodedToken(User user)
        {
            var claims = GenerateClaimsIdentity(user);
            return GenerateEncodedToken(claims);
        }

        private string GenerateEncodedToken(ClaimsIdentity identity)
        {
            var claims = identity.Claims.Union(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identity.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString()),
            });

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                Jwt.Issuer,
                Jwt.Audience,
                claims,
                DateTime.Now,
                DateTime.Now + Jwt.TokenLifetime,
                new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }


        private ClaimsIdentity GenerateClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimType.UserId, user.Id.ToString()),
                new Claim(JwtClaimType.Email, user.Email),
                new Claim(JwtClaimType.DisplayName, user.DisplayName)
            };

            return new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);
        }
    }
}
