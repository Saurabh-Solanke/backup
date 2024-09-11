namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }

        Task<int> SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
