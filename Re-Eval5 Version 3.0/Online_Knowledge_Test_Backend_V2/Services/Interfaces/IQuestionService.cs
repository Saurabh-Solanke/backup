using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task CreateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
    }
}
