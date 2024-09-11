using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge_Test_Backend_V2.Models;  

namespace Online_Knowledge_Test_Backend_V2.RepositoryLayer.Implementation
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ExamDbContext _context;

        public SectionRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync()
        {
            return await _context.Sections
                .Include(s => s.Questions)
                .ToListAsync();
        }

        public async Task<Section> GetSectionByIdAsync(int sectionId)
        {
            return await _context.Sections
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.SectionId == sectionId);
        }

        public async Task<IEnumerable<Section>> GetSectionsByExamIdAsync(int examId)
        {
            return await _context.Sections
                .Where(s => s.ExamId == examId)
                .Include(s => s.Questions)
                .ToListAsync();
        }

        public async Task CreateSectionAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(Section section)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            var section = await _context.Sections.FindAsync(sectionId);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> CalculateTotalMarksAsync(int sectionId)
        {
            var section = await _context.Sections
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.SectionId == sectionId);

            if (section == null)
                return 0;

            // Calculate total marks by summing correct option marks
            int totalMarks = section.Questions.Sum(q =>
                q.Options.Where(o => o.IsCorrect && o.Marks > 0).Sum(o => o.Marks ?? 0)); // Consider only positive marks



            return totalMarks;
        }
    }
}
