using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IJWTTokenService
    {
        Task<string> GenerateToken(User user);
    }
}
