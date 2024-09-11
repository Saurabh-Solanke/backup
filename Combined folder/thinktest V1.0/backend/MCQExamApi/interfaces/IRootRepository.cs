using MCQExamApi.Models;

namespace MCQExamApi.interfaces
{
    public interface IRootRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task<TEntity> DeleteAsync(int id);

        IQueryable<TEntity> GetQueryable();

        Task<IEnumerable<Option>> GetCorrectOptionsByQuestionIdAsync(int questionId);

        Task<IEnumerable<Result>> GetResultByStudentExamIdAsync(int studentExamId);
        Task<IEnumerable<StudentAnswer>> GetStudentAnswerByStudentExamIdAsync(int studentExamId); 
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
    }
}
