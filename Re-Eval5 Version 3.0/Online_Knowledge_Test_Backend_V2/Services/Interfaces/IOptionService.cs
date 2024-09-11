using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.Services.Interfaces
{
    public interface IOptionService
    {
        Task CreateOptionAsync(Option option);
        Task UpdateOptionAsync(Option option);
        Task DeleteOptionAsync(int optionId);
    }
}
