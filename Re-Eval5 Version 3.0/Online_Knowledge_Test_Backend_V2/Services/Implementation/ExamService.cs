using AutoMapper;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync()
        {
            var exams = await _examRepository.GetAllExamsAsync();
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<ExamDto> GetExamByIdAsync(int id)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                return null;

            return _mapper.Map<ExamDto>(exam);
        }

        public async Task<ExamDto> CreateExamAsync(CreateExamDto createExamDto)
        {
            var exam = _mapper.Map<Exam>(createExamDto);
            exam.CreatedDate = DateTime.UtcNow;
            exam.IsPublished = true; // Set IsPublished to true by default

            await _examRepository.CreateExamAsync(exam);

            return _mapper.Map<ExamDto>(exam);
        }

        public async Task UpdateExamAsync(int id, UpdateExamDto updateExamDto)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                throw new KeyNotFoundException("Exam not found");

            _mapper.Map(updateExamDto, exam);

            await _examRepository.UpdateExamAsync(exam);
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                throw new KeyNotFoundException("Exam not found");

            await _examRepository.DeleteExamAsync(id);
        }
    }
}
