using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Implementation
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ExamDbContext _context;

        public OptionRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task CreateOptionAsync(Option option)
        {
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOptionAsync(Option option)
        {
            _context.Options.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOptionAsync(int optionId)
        {
            var option = await _context.Options.FindAsync(optionId);
            if (option != null)
            {
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
        }
    }
}
