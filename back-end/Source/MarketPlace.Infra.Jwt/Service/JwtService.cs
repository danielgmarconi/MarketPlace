using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Jwt.Context;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.Infra.Jwt.Service
{
    public class JwtService : IJwtService
    {
        private readonly ApplicationJwtContext _applicationJwtContext;
        public JwtService(ApplicationJwtContext context)
        {
            _applicationJwtContext = context;
        }
        public IEnumerable<Claim> ClaimsGenerate(int userid,
                                                 string email) => new[]{ new Claim("userid", userid.ToString()),
                                                                         new Claim("email", email),
                                                                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        public string GenerateToken(int userid, string email)
        {
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationJwtContext.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_applicationJwtContext.ExpiresMinutes);
            JwtSecurityToken token = new JwtSecurityToken( issuer: _applicationJwtContext.Issuer,
                                                           audience: _applicationJwtContext.Audience,
                                                           claims: ClaimsGenerate(userid, email),
                                                           expires: expiration,
                                                           signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
