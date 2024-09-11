using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class UserTestAnswerRepository : Repository<UserTestAnswer>, IUserTestAnswerRepository
    {

        private readonly ExamDbContext _context;

        public UserTestAnswerRepository(ExamDbContext context) : base(context)
        {
            _context = context;
        }

        // Get all answers for a specific test
        public async Task<IEnumerable<UserTestAnswer>> GetAnswersByTestIdAsync(int testId)
        {
            return await _context.UserTestAnswers
                .Where(uta => uta.TestId == testId)
                .ToListAsync();
        }

        // Get answers for a specific user and test
        public async Task<IEnumerable<UserTestAnswer>> GetAnswersByUserIdAndTestIdAsync(int userId, int testId)
        {
            return await _context.UserTestAnswers
                .Where(uta => uta.TestId == testId && uta.Test.UserId == userId)
                .ToListAsync();
        }

        // Add a single UserTestAnswer to the database
        public async Task AddAsync(UserTestAnswer userTestAnswer)
        {
            await _context.UserTestAnswers.AddAsync(userTestAnswer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

