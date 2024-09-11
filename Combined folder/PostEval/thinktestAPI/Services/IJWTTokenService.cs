using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.Services
{
    public interface IJWTTokenService
    {
        Task<string> GenerateToken(ExamUser user);
    }
}
