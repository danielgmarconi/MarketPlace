using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketPlace.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.Infra.Jwt.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Claim> ClaimsGenerate(int userid,
                                                 string email) => new[]{ new Claim("userid", userid.ToString()),
                                                                         new Claim("email", email),
                                                                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        public string GenerateToken(int userid, string email)
        {
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secretkey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiresMinutes"]));
            JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                                                           audience: _configuration["Jwt:Audience"],
                                                           claims: ClaimsGenerate(userid, email),
                                                           expires: expiration,
                                                           signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
