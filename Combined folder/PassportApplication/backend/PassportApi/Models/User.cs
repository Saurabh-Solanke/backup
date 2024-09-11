using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [ForeignKey("PassportUser")]
        public string PassportUserId { get; set; } 


        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; }

        // Navigation properties
        public PassportUser PassportUser { get; set; }
        public ICollection<MasterDetailsTable> Applications { get; set; }
        public ICollection<FeedbackComplaint> Feedbacks { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
