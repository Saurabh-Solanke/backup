using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionsAsync();
        Task<Section> GetSectionByIdAsync(int sectionId);
        Task<IEnumerable<Section>> GetSectionsByExamIdAsync(int examId); // Existing method to get sections by ExamId
        Task CreateSectionAsync(Section section);
        Task UpdateSectionAsync(Section section);
        Task DeleteSectionAsync(int sectionId);
        Task<int> CalculateTotalMarksAsync(int sectionId);
    }
}
