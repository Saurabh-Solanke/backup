using Microsoft.AspNetCore.Identity;
using Online_Knowledge__Test_Backend.DTOs;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IAuthService
    { 
        Task<LoggedInUser> Login(LoginDto model);

        Task<IdentityResult> Register(RegisterDto model);
    }
}
