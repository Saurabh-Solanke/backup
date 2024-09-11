using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.Data
{
    public class ExamDbContext : IdentityDbContext<ExamUser>
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> builder) : base(builder) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Result> Results { get; set; }
    
        public DbSet<Section> Sections { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<PassingThreshold> PassingThresholds { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<TestHistory> TestHistories { get; set; }
        public DbSet<SectionQuestion> SectionQuestions { get; set; }
        public DbSet<UserTestAnswer> UserTestAnswers { get; set; }
 
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Specify precision for decimal properties
            builder.Entity<PassingThreshold>()
                .Property(p => p.ThresholdPercentage)
                .HasPrecision(5, 2); // 5 total digits, 2 decimal places

            builder.Entity<Result>()
                .Property(r => r.PercentageObtained)
                .HasPrecision(5, 2); // 5 total digits, 2 decimal places

            builder.Entity<TestHistory>()
                .Property(t => t.FinalScore)
                .HasPrecision(5, 2); // 5 total digits, 2 decimal places

            List<IdentityRole> Roles = new List<IdentityRole>
            {
                new IdentityRole(){
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole(){
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(Roles);  //adding admin and user role in role table
        }

    }
}
