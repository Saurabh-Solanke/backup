using AutoMapper;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionService(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectionGetDTO>> GetAllSectionsAsync()
        {
            var sections = await _sectionRepository.GetAllSectionsAsync();
            return _mapper.Map<IEnumerable<SectionGetDTO>>(sections);
        }

        public async Task<SectionGetDTO> GetSectionByIdAsync(int sectionId)
        {
            var section = await _sectionRepository.GetSectionByIdAsync(sectionId);
            return _mapper.Map<SectionGetDTO>(section);
        }

        public async Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId)
        {
            var sections = await _sectionRepository.GetSectionsByExamIdAsync(examId);
            return _mapper.Map<IEnumerable<SectionGetDTO>>(sections);
        }

        public async Task CreateSectionAsync(SectionPostDTO sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            await _sectionRepository.CreateSectionAsync(section);
        }

        public async Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto)
        {
            var section = await _sectionRepository.GetSectionByIdAsync(sectionId);
            if (section != null)
            {
                _mapper.Map(sectionDto, section);
                await _sectionRepository.UpdateSectionAsync(section);
            }
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            await _sectionRepository.DeleteSectionAsync(sectionId);
        }

        public async Task<int> CalculateTotalMarksAsync(int sectionId)
        {
            return await _sectionRepository.CalculateTotalMarksAsync(sectionId);
        }
    }
}
