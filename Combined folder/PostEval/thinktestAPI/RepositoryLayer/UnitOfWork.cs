using Online_Knowledge__Test_Backend.Data;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _examDbContext;

        public IUserRepository UserRepository { get; }

        public ITestRepository TestRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IResultRepository ResultRepository { get; }

        public IUserTestAnswerRepository UserTestAnswerRepository { get; }


        public UnitOfWork(ExamDbContext examDbContext)
        {
            this.UserRepository = new UserRepository(examDbContext);
            this.TestRepository = new TestRepository(examDbContext);
            this.SubjectRepository = new SubjectRepository(examDbContext);
            this.ResultRepository = new ResultRepository(examDbContext);
            this.UserTestAnswerRepository = new UserTestAnswerRepository(examDbContext);
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
