namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public ITestRepository TestRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IResultRepository ResultRepository { get; }
        public IUserTestAnswerRepository UserTestAnswerRepository { get; }

        Task<int> SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
