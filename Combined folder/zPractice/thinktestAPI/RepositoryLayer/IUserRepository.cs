using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmail(string email);
    }
}
