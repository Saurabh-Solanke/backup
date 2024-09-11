using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class TestRepository : Repository<Test>, ITestRepository
    {


        private readonly ExamDbContext _context;
        public TestRepository(ExamDbContext examDbContext) : base(examDbContext)
        {
            _context = examDbContext;
        }



        public async Task<IEnumerable<QuestionAndAnswerDTO>> GetAllQuestionAnswer()
        {
            var result = new List<QuestionAndAnswerDTO>();

            // Query database to get questions with their options
            var questions = await _context.Questions
                .Include(q => q.Options)
                .ToListAsync();


            foreach (var question in questions)
            {
                // Convert Options to a List if it's not already
                var optionsList = question.Options.ToList();

                // Collect indices of correct answers
                var correctAnswers = optionsList
                    .Select((o, index) => o.IsCorrect ? index : -1)
                    .Where(index => index != -1)
                    .ToArray();

                result.Add(new QuestionAndAnswerDTO
                {
                    Id = question.QuestionId,
                    Question = question.QuestionText,
                    Options = optionsList.Select(o => o.OptionText).ToList(),
                    Answer = correctAnswers, // Index of the correct option
                    IsMultiple = correctAnswers.Length > 1
                });
            }

            return result;
        }
        public async Task<SubjectDTO> GetQuestionsBySubjectIdAsync(int subjectId)
        {
            // Query database to get the subject and its questions with their options
            var subject = await _context.Subjects
                .Where(s => s.SubjectId == subjectId)
                .Select(s => new
                {
                    s.SubjectId,
                    s.SubjectName,
                    Questions = s.Questions.Select(q => new
                    {
                        q.QuestionId,
                        q.QuestionText,
                        Options = q.Options.Select(o => new
                        {
                            o.OptionText,
                            o.IsCorrect
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new KeyNotFoundException("Subject not found");
            }

            var result = new SubjectDTO
            {
                Id = subject.SubjectId,
                Name = subject.SubjectName,
                Questions = subject.Questions.Select(q => new QuestionDTO
                {
                    Id = q.QuestionId,
                    Question = q.QuestionText,
                    Options = q.Options.Select(o => o.OptionText).ToList(),
                    Answer = q.Options.Select((o, index) => o.IsCorrect ? index : -1)
                                  .Where(index => index != -1)
                                  .ToArray(), // Index of the correct option
                    IsMultiple = q.Options.Count(o => o.IsCorrect) > 1
                }).ToList()
            };

            return result;
        }

        public async Task<Test> SaveTaskData(Test test)
        {
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();
            return test;
        }





    }
}
