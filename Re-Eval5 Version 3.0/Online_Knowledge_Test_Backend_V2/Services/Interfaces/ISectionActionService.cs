using Online_Knowledge_Test_Backend_V2.DTOs;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface ISectionActionService
    {
        Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId);
        Task<SectionGetDTO> GetSectionByIdAsync(int sectionId);
        Task CreateSectionAsync(SectionPostDTO sectionDto);
        Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto);
        Task DeleteSectionAsync(int sectionId);
        Task AddQuestionAsync(int sectionId, QuestionPostDTO questionDto);
        Task DeleteQuestionAsync(int questionId);
    }
}
