using Microsoft.AspNetCore.Identity;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _examDbContext;
        private readonly UserManager<User> _userManager;


        public IUserRepository UserRepository { get; }


        public UnitOfWork(ExamDbContext examDbContext, UserManager<User> userManager)
        {
            _examDbContext = examDbContext;
            _userManager = userManager;
            UserRepository = new UserRepository(examDbContext, userManager);
        }


        public void Dispose()
        {
            _examDbContext.Dispose();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
