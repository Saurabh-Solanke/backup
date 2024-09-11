using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class SectionActionService : ISectionActionService
    {
        private readonly ISectionActionRepository _sectionActionRepository;

        public SectionActionService(ISectionActionRepository sectionActionRepository)
        {
            _sectionActionRepository = sectionActionRepository;
        }

        public async Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId)
        {
            return await _sectionActionRepository.GetSectionsByExamIdAsync(examId);
        }

        public async Task<SectionGetDTO> GetSectionByIdAsync(int sectionId)
        {
            return await _sectionActionRepository.GetSectionByIdAsync(sectionId);
        }

        public async Task CreateSectionAsync(SectionPostDTO sectionDto)
        {
            await _sectionActionRepository.CreateSectionAsync(sectionDto);
        }

        public async Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto)
        {
            await _sectionActionRepository.UpdateSectionAsync(sectionId, sectionDto);
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            await _sectionActionRepository.DeleteSectionAsync(sectionId);
        }

        public async Task AddQuestionAsync(int sectionId, QuestionPostDTO questionDto)
        {
            await _sectionActionRepository.AddQuestionAsync(sectionId, questionDto);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            await _sectionActionRepository.DeleteQuestionAsync(questionId);
        }
    }
}
