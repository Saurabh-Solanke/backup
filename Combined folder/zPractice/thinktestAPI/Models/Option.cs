using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        public string OptionText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }

        // options * --> question 1  
        public int QuestionId { get; set; }
        public Question Question { get; set; }  // reference navigation property

    }
}
