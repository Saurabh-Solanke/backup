using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class FeedbackComplaintDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select one options")]
        public FeedbackComplaintType FeedbackComplaintType { get; set; }  // Feedback/Complaint

        [Required(ErrorMessage = "Please enter the Message")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter the email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the message here")]
        public string Description { get; set; }
        public ComplaintStatus? ComplaintStatus { get; set; }

        public int UserId { get; set; }

    }
}
