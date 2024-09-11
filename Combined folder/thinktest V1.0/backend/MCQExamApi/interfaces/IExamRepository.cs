using MCQExamApi.Dtos.Exam;

namespace MCQExamApi.interfaces
{
    public interface IExamRepository
    {
        Task<ExamRespDTO> GetExamDetailsById(int id);
    }
}
