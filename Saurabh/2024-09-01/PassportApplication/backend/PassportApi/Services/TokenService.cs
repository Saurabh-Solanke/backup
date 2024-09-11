

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PassportApi.interfaces;
using PassportApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PassportApi.Services
{
    

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<PassportUser> _userManager;

        public TokenService(IConfiguration config, UserManager<PassportUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        public async Task<string> CreateToken(PassportUser passUser)
        {
            var claims = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Email,passUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,passUser.UserName)
            };

            // Add role claims
            var roles =await _userManager.GetRolesAsync(passUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token =tokenHandler.CreateToken(tokenDescriptor); ;//till here it will make an obj represenation of token

            return tokenHandler.WriteToken(token);//string representatiton of token
        }
    }
}
