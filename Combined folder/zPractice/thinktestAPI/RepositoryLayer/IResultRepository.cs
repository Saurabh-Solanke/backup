using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface IResultRepository : IRepository<Result>
    {
        Task<IEnumerable<UserResultDto>> GetUserResult(int id);
    }
}
