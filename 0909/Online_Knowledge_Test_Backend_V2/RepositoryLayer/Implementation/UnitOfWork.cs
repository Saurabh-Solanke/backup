using Online_Knowledge_Test_Backend_V2.Data;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _examDbContext;

        public IUserRepository UserRepository { get; }


        public UnitOfWork(ExamDbContext examDbContext)
        {
            this.UserRepository = new UserRepository(examDbContext);
            _examDbContext = examDbContext;
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
