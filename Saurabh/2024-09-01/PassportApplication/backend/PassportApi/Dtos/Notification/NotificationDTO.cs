using PassportApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Dtos.Notification
{
    public class NotificationDTO
    {
        public int NotificationID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string NotificationMessage { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public bool IsRead { get; set; }
    }
}
