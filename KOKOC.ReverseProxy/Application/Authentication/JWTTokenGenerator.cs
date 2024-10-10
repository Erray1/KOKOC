using KOKOC.ReverseProxy.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KOKOC.ReverseProxy.Application.Authentication
{
    public class JWTTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _config;
        public JWTTokenGenerator(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Generate(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:Inner:JWTKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Authentication:Inner:JWTIssuer"]!,
                audience: _config["Authentication:Inner:JWTAudience"]!,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Authentication:Inner: JWTLifetimeMinutes")),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
