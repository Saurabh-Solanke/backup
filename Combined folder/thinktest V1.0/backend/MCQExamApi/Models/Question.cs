using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string QuestionType { get; set; } // Enum: SingleAnswer, MultipleAnswers

        public int? Points { get; set; }


        //Nav
        public Exam Exam { get; set; }

        public ICollection<Option> Options { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
