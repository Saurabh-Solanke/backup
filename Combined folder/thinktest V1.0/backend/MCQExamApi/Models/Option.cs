using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }


        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [Required]
        public string OptionText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        //Nav
        public Question Question { get; set; }
    }
}
