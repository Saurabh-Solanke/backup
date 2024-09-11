using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class UserTestAnswerDto
    {
        [Required]
        public int TestId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int OptionId { get; set; }

        [Required]
        public bool IsSelected { get; set; }
    }
}
