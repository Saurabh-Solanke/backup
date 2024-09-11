using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge_Test_Backend_V2.Models;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace Online_Knowledge_Test_Backend_V2.Data
{
    public class ExamDbContext : IdentityDbContext<User>
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> builder) : base(builder) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
