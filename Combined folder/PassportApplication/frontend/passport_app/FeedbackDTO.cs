using PassportAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportAPI.DTO
{
    public class FeedbackDTO
    {
        [Required(ErrorMessage = "Please select whether you want to lauch a complaint or feedback!!!")]
        public string Type { get; set; }
        public string Email { get; set; }                 //Autofilled property on UI

        [Required(ErrorMessage = "Please provide title for complain/feedback")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please provide us with description")]
        public string Description { get; set; }
        public Status Status { get; set; }              //Not to be filled by user & default value is Pending
        public DateTime CreatedOn { get; set; }         //Not to be filled by user
        public DateTime UpdatedOn { get; set; }         //Not to be filled by user
        public int UserID { get; set; }
    }
}
