using Microsoft.EntityFrameworkCore;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
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
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;

        }
    }
}
