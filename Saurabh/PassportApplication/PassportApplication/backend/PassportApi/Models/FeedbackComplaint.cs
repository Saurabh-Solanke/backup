using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PassportApi.Models.Enums;

namespace PassportApi.Models
{
    public class FeedbackComplaint
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Select one options")]
        public FeedbackComplaintType FeedbackComplaintType { get; set; }  // Feedback/Complaint


        [Required(ErrorMessage = "Please Enter the email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the Message")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the message here")]
        public string Description { get; set; }
        public ComplaintStatus? ComplaintStatus { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
