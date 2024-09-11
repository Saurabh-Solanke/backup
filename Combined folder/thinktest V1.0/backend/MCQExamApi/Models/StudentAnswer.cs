using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class StudentAnswer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        [ForeignKey("StudentExam")]
        public int StudentExamId { get; set; }

        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Option")]
        public int? SelectedOptionId { get; set; } // Nullable for SingleAnswer

        public string? SelectedOptionIds { get; set; } // Comma-separated for MultipleAnswers


        //Nav
        public StudentExam StudentExam { get; set; }

        public Question Question { get; set; }

        public Option SelectedOption { get; set; }
    }
}
