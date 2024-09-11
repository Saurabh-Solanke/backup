using Microsoft.EntityFrameworkCore;
using PassPortal_API.Models;

namespace PassPortal_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PassportOffice> PassportOffices { get; set; }
    }
}
