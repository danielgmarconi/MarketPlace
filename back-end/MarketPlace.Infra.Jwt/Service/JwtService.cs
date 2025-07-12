using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Jwt.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.Infra.Jwt.Service
{
    public class JwtService : IJwtService
    {
        private readonly JwtContext _jwtContext;
        public JwtService(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            if (!jwtSection.Exists())
                throw new Exception("Jwt config section not found.");
            _jwtContext = jwtSection.Get<JwtContext>();
        }
        public IEnumerable<Claim> ClaimsGenerate(int userid,
                                                 string email) => new[]{ new Claim("userid", userid.ToString()),
                                                                         new Claim("email", email),
                                                                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        public string GenerateToken(int userid, string email)
        {
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtContext.Secretkey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtContext.ExpiresMinutes);
            JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtContext.Issuer,
                                                           audience: _jwtContext.Audience,
                                                           claims: ClaimsGenerate(userid, email),
                                                           expires: expiration,
                                                           signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
