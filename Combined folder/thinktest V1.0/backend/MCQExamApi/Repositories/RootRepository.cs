using MCQExamApi.Data;
using MCQExamApi.interfaces;
using MCQExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MCQExamApi.Repositories
{
    public class RootRepository<TEntity> : IRootRepository<TEntity> where TEntity : class
    {
        public readonly ExamContext _context;

        public RootRepository(ExamContext context)
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

        public  IQueryable<TEntity> GetQueryable()
        {
            return  _context.Set<TEntity>().AsQueryable();
        }

        public async Task<IEnumerable<Option>> GetCorrectOptionsByQuestionIdAsync(int questionId)
        {
            return await _context.Options
                .Where(option => option.QuestionId == questionId && option.IsCorrect)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetResultByStudentExamIdAsync(int studentExamId)
        {
            return await _context.Results
            .Where(r => r.StudentExamId == studentExamId)
            .ToListAsync();
        }

        public async Task<IEnumerable<StudentAnswer>> GetStudentAnswerByStudentExamIdAsync(int studentExamId)
        {
            return await _context.StudentAnswers
           .Where(sa => sa.StudentExamId == studentExamId)
           .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId)
        {
            return await _context.Questions
            .Where(q => q.ExamId == examId)
            .Include(q => q.Options) // Include related options
            .ToListAsync();
        }
    }
}
