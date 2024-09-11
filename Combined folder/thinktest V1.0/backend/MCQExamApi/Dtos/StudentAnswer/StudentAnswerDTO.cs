using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.StudentAnswer
{
    public class StudentAnswerDTO
    {
        public int AnswerId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "StudentExamId must be a positive integer.")]
        public int? StudentExamId { get; set; }


        [Required(ErrorMessage = "QuestionId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "QuestionId must be a positive integer.")]
        public int QuestionId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "SelectedOptionId must be a positive integer.")]
        public int? SelectedOptionId { get; set; } // Nullable for SingleAnswer
        

        [RegularExpression(@"^\d+(,\d+)*$", ErrorMessage = "SelectedOptionIds must be a comma-separated list of integers.")]
        public string? SelectedOptionIds { get; set; } // Comma-separated for MultipleAnswers
    }
}
