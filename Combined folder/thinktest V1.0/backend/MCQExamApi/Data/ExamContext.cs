using MCQExamApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MCQExamApi.Data
{
    public class ExamContext : IdentityDbContext<ExamUser>
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between StudentAnswers and Questions
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Question)
                .WithMany(q => q.StudentAnswers)
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes to avoid cycles

            // Configure the relationship between StudentAnswers and StudentExams
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.StudentExam)
                .WithMany(se => se.StudentAnswers) // Assuming StudentExam has a collection of StudentAnswers
                .HasForeignKey(sa => sa.StudentExamId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes to avoid cycles

            // Configure StudentExams to Users relationship to prevent cascade delete
            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Student) // Assuming Student is a User with a role
                .WithMany(u => u.StudentExams)
                .HasForeignKey(se => se.StudentId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes to avoid cycles

            // Configure one-to-many relationship between User (Teacher) and Exams
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatedExams)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes to avoid cycles
        }
    }
}
