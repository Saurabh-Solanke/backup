using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [Required(ErrorMessage = "Result ID is required.")]
        public int ResultId { get; set; }  // Foreign Key to ExamResult

        [Required(ErrorMessage = "Question ID is required.")]
        public int QuestionId { get; set; }

        // This field can be null if the user did not select an option
        public int? SelectedOptionId { get; set; }

        // Navigation properties
        public ExamResult ExamResult { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
    }
}
