using MCQExamApi.Models;

namespace MCQExamApi.interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(ExamUser user);
    }
}
