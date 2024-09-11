using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;    

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ExamDbContext _context;

        public QuestionRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.Options)
                .ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId)
        {
            return await _context.Questions
                .Where(q => q.Section.ExamId == examId)
                .Include(q => q.Options)  // Include options to check correct answers
                .ToListAsync();
        }

        public async Task<IEnumerable<Section>> GetSectionsWithQuestionsAsync(int examId)
        {
            return await _context.Sections
                .Where(s => s.ExamId == examId)
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .ToListAsync();
        }

        public async Task<List<Option>> GetOptionsByQuestionIdAsync(int questionId)
        {
            return await _context.Options
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();
        }
    }
}
