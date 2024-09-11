using Online_Knowledge_Test_Backend_V2.DTOs;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionGetDTO>> GetAllSectionsAsync();
        Task<SectionGetDTO> GetSectionByIdAsync(int sectionId);
        Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId);
        Task CreateSectionAsync(SectionPostDTO sectionDto);
        Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto);
        Task DeleteSectionAsync(int sectionId);
        Task<int> CalculateTotalMarksAsync(int sectionId);
    }
}
