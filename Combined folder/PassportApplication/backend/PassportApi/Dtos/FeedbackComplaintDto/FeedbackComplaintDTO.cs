using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.FeedbackComplaintDto
{
    public class FeedbackComplaintDTO
    {

        [Required(ErrorMessage = "Please select whether you want to lauch a complaint or feedback!!!")]
        public FeedbackComplaintType Type { get; set; }          //Autofilled property on UI

        [Required(ErrorMessage = "Please provide title for complain/feedback")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please provide us with description")]
        public string Description { get; set; }
        public ComplaintStatus? Status { get; set; }              //Not to be filled by user & default value is Pending
        public DateTime CreatedOn { get; set; } = DateTime.Now;         //Not to be filled by user
        public DateTime UpdatedOn { get; set; } = DateTime.Now;       //Not to be filled by user
        public int UserID { get; set; }
    }
}
