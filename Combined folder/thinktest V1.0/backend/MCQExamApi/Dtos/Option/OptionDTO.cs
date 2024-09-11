using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.Option
{
    public class OptionDTO
    {
        public int OptionId { get; set; }
        public int? QuestionId { get; set; }


        [Required(ErrorMessage = "OptionText is required.")]
        [StringLength(350, ErrorMessage = "OptionText cannot be longer than 200 characters.")]
        public string OptionText { get; set; }


        [Required(ErrorMessage = "IsCorrect is required.")]
        public bool IsCorrect { get; set; }
    }
}
