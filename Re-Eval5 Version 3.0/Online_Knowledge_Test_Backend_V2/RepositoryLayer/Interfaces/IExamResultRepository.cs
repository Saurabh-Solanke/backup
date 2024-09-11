using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces
{
    public interface IExamResultRepository
    {
        Task<ExamResult> SubmitExamResultAsync(ExamResult examResult);
        Task AddUserAnswersAsync(IEnumerable<UserAnswer> userAnswers);
        Task<int> GetLatestAttemptNumberAsync(string userId, int examId);
        Task<IEnumerable<ExamResult>> GetAllResultsAsync();
        Task<IEnumerable<ExamResult>> GetResultsByUserIdAsync(string userId);
        Task AddSectionResultsAsync(IEnumerable<SectionResult> sectionResults);
    }
}
