using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PassportApi.Models;

namespace PassportApi.Data
{
    public class PassportDbContext : IdentityDbContext<PassportUser>
    {
        public PassportDbContext(DbContextOptions options) : base(options)
        {

           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ServiceRequired> ServiceRequireds { get; set; }
        public DbSet<ApplicantDetails> ApplicantDetails { get; set; }
        public DbSet<FamilyDetails> FamilyDetails { get; set; }
        public DbSet<AddressTable> AddressTables { get; set; }
        public DbSet<EmergencyContactDetails> EmergencyContactDetails { get; set; }
        public DbSet<OtherDetails> OtherDetails { get; set; }
        public DbSet<DocumentTable> DocumentTables { get; set; }
        public DbSet<MasterDetailsTable> MasterDetailsTables { get; set; }
        public DbSet<FeedbackComplaint> FeedbackComplaints { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }

    
}
