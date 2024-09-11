using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<ExamResult>> GetTestsPerDay()
        {
            return await _reportRepository.GetTestsPerDay();
        }

        public async Task<IEnumerable<ReportTwoDto>> GetTestsFinishedBeforeTime()
        {
            return await _reportRepository.GetTestsFinishedBeforeTime();
        }

        public async Task<IEnumerable<ReportThreeDto>> GetAutoSubmittedTests()
        {
            return await _reportRepository.GetAutoSubmittedTests();
        }

        public async Task<IEnumerable<ExamResultDto>> GetMarkedForReviewTests()
        {
            return await _reportRepository.GetMarkedForReviewTests();
        }

        public async Task<IEnumerable<QuestionImageReportDto>> GetQuestionsWithImages()
        {
            return await _reportRepository.GetQuestionsWithImages();
        }

        public async Task<IEnumerable<QuestionVideoReportDto>> GetQuestionsWithVideos()
        {
            return await _reportRepository.GetQuestionsWithVideos();
        }

        public async Task<int> GetImageQuestionsCount()
        {
            return await _reportRepository.GetImageQuestionsCount();
        }

        public async Task<int> GetVideoQuestionsCount()
        {
            return await _reportRepository.GetVideoQuestionsCount();
        }

        public async Task<IEnumerable<TopStudentDto>> GetTop10StudentsByPercentile(int examId)
        {
            return await _reportRepository.GetTop10StudentsByPercentile(examId);
        }

        public async Task<IEnumerable<ExamResult>> GetTestsPerDayInRange(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetTestsPerDayInRange(startDate, endDate);
        }

        public async Task<IEnumerable<ReportTwoDto>> GetTestsFinishedBeforeTimeInRange(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetTestsFinishedBeforeTimeInRange(startDate, endDate);
        }

        public async Task<IEnumerable<ReportThreeDto>> GetAutoSubmittedTestsInRange(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetAutoSubmittedTestsInRange(startDate, endDate);
        }

        public async Task<IEnumerable<ExamResultDto>> GetMarkedForReviewTestsInRange(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetMarkedForReviewTestsInRange(startDate, endDate);
        }
    }   
}
