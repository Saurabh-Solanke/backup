using Microsoft.EntityFrameworkCore;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Microsoft.AspNetCore.Identity;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ExamDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(ExamDbContext examDbContext, UserManager<User> userManager) : base(examDbContext)
        {
            _dbContext = examDbContext;
            _userManager = userManager;
        }

        public async Task<User> FindByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;

        }
    }
}
