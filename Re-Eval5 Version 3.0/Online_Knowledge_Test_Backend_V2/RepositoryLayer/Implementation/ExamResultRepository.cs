using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Implementation
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly ExamDbContext _context;

        public ExamResultRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<ExamResult> SubmitExamResultAsync(ExamResult examResult)
        {
            await _context.ExamResults.AddAsync(examResult);
            await _context.SaveChangesAsync();
            return examResult;
        }

        public async Task AddUserAnswersAsync(IEnumerable<UserAnswer> userAnswers)
        {
            await _context.UserAnswers.AddRangeAsync(userAnswers);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLatestAttemptNumberAsync(string userId, int examId)
        {
            var latestAttempt = await _context.ExamResults
                .Where(er => er.UserId == userId && er.ExamId == examId)
                .OrderByDescending(er => er.AttemptNumber)
                .FirstOrDefaultAsync();

            return latestAttempt?.AttemptNumber ?? 0;  // Return 0 if no previous attempts exist
        }

        // New method to get all exam results
        public async Task<IEnumerable<ExamResult>> GetAllResultsAsync()
        {
            return await _context.ExamResults
                .Include(er => er.Exam)  // Optional: Include related exam data
                .Include(er => er.User)  // Optional: Include related user data
                .ToListAsync();
        }

        // New method to get exam results by UserId
        public async Task<IEnumerable<ExamResult>> GetResultsByUserIdAsync(string userId)
        {
            return await _context.ExamResults
                .Where(er => er.UserId == userId)
                .Include(er => er.Exam)  // Optional: Include related exam data
                .ToListAsync();
        }

        public async Task AddSectionResultsAsync(IEnumerable<SectionResult> sectionResults)
        {
            _context.SectionResults.AddRange(sectionResults);
            await _context.SaveChangesAsync();
        }

    }
}
