using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ExamDbContext _dbContext;
        public UserRepository(ExamDbContext examDbContext) : base(examDbContext)
        {
            _dbContext = examDbContext;
        }

        public async Task<User> FindByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
            return user;

        }
    }
}
