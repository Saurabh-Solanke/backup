using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.interfaces;
namespace PassportApi.Repositories
{
    public class RootRepository<TEntity> : IRootRepository<TEntity> where TEntity : class
    {
        public readonly PassportDbContext _context;

        public RootRepository(PassportDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {

            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity updatedEntity)
        {

            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                return null; // Entity not found
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);

            // Now only the properties that have been modified will be updated.
            await _context.SaveChangesAsync();

            return existingEntity;
        }
    }
}
