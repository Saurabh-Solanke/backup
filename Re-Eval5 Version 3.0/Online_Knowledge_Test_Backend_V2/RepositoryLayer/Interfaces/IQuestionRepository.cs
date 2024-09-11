using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
        Task<IEnumerable<Section>> GetSectionsWithQuestionsAsync(int examId);
        Task<List<Option>> GetOptionsByQuestionIdAsync(int questionId);
    }
}
