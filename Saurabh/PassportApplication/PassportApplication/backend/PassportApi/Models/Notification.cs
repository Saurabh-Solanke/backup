using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string NotificationMessage { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public bool IsRead { get; set; }
    }
}
