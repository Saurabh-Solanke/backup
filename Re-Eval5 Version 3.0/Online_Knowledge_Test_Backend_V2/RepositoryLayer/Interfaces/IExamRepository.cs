using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync();
        Task<Exam> GetExamByIdAsync(int examId);
        Task CreateExamAsync(Exam exam);
        Task UpdateExamAsync(Exam exam);
        Task DeleteExamAsync(int examId);
    }   
}
