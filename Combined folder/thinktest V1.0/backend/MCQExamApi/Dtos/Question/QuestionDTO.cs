using MCQExamApi.Dtos.Option;
using MCQExamApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.Question
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }


        [Required(ErrorMessage = "ExamId is required.")]
        public int ExamId { get; set; }


        [Required(ErrorMessage = "QuestionText is required.")]
        [StringLength(500, ErrorMessage = "QuestionText cannot be longer than 500 characters.")]
        public string QuestionText { get; set; }


        [Required(ErrorMessage = "QuestionType is required.")]
        [RegularExpression("SingleAnswer|MultipleAnswers", ErrorMessage = "QuestionType must be either 'SingleAnswer' or 'MultipleAnswers'.")]
        public string QuestionType { get; set; } // Enum: SingleAnswer, MultipleAnswers


        [Range(1, int.MaxValue, ErrorMessage = "Points must be a positive integer.")]
        public int? Points { get; set; }

        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();
    }
}
