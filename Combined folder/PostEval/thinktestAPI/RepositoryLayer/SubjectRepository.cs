using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository

    {
        private readonly ExamDbContext _context;
        public SubjectRepository(ExamDbContext examDbContext) : base(examDbContext)
        {
            _context = examDbContext;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

    }
}
