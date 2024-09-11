using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces
{
    public interface IOptionRepository
    {
        Task CreateOptionAsync(Option option);
        Task UpdateOptionAsync(Option option);
        Task DeleteOptionAsync(int optionId);
    }
}
