using Online_Knowledge_Test_Backend_V2.DTOs;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetAllExamsAsync();
        Task<ExamDto> GetExamByIdAsync(int id);
        Task<ExamDto> CreateExamAsync(CreateExamDto createExamDto);
        Task UpdateExamAsync(int id, UpdateExamDto updateExamDto);
        Task DeleteExamAsync(int id);
    }
}
