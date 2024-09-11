using Online_Knowledge_Test_Backend_V2.DTOs;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IExamResultService
    {
        Task<SubmitExamResultDto> SubmitResultAsync(SubmitResultDto submitResultDto);
        Task<IEnumerable<SubmitExamResultDto>> GetAllResultsAsync();
        Task<IEnumerable<SubmitExamResultDto>> GetResultsByUserAsync(string userId);
    }
}
