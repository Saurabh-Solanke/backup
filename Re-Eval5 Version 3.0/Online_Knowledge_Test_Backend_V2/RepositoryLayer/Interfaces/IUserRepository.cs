

using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmail(string email);
    }
}
