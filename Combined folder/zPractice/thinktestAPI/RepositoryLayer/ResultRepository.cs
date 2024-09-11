using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        private readonly ExamDbContext _context;
        public ResultRepository(ExamDbContext examDbContext) : base(examDbContext)
        {
            _context = examDbContext;
        }

        public async Task<IEnumerable<UserResultDto>> GetUserResult(int id)
        {
            var results = await _context.Results.Where(r => r.UserId == id).OrderByDescending(r => r.ResultId)
                    .Select(r => new UserResultDto
                    {
                        SubjectName = r.Test.SubjectName,
                        TotalQuestions = r.TotalQuestions,
                        AttemptedQuestions = r.AttemptedQuestions,
                        CorrectQuestions = r.CorrectQuestions,
                        InCorrectQuestions = r.InCorrectQuestions,
                        UnAttemptedQuestions = r.UnAttemptedQuestions,
                        PercentageObtained = r.PercentageObtained,
                    }).ToListAsync();
            return results;

        }
    }
}
