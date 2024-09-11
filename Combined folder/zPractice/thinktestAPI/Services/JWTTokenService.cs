using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Online_Knowledge__Test_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Knowledge__Test_Backend.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly UserManager<ExamUser> _userManager;
        private readonly IConfiguration _config;

        public JWTTokenService(UserManager<ExamUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> GenerateToken(ExamUser userData)
        {
            var userRoles = await _userManager.GetRolesAsync(userData);
            var authClaims = new List<Claim>
         {
             new Claim(ClaimTypes.Role, userData.UserName),
            
         };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
