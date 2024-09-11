using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<IEnumerable<QuestionAndAnswerDTO>> GetAllQuestionAnswer();

        Task<SubjectDTO> GetQuestionsBySubjectIdAsync(int subjectId);
        Task<Test> SaveTaskData(Test test);
    }
}
