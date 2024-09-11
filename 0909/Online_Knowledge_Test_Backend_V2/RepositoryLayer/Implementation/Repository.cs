using Microsoft.EntityFrameworkCore;
using Online_Knowledge_Test_Backend_V2.Data;

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ExamDbContext _examDbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ExamDbContext examDbContext)
        {
            _examDbContext = examDbContext;
            _dbSet = examDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _examDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {

            _dbSet.Remove(entity);
            await _examDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _examDbContext.Update(entity);
            await _examDbContext.SaveChangesAsync();
        }
    }
}
