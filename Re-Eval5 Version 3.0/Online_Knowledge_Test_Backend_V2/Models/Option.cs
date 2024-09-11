using Online_Knowledge_Test_Backend_V2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        [Required(ErrorMessage = "Question ID is required.")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Option Text is required.")]
        [StringLength(500, ErrorMessage = "Option Text cannot exceed 500 characters.")]
        public string OptionText { get; set; }

        [Required(ErrorMessage = "Is Correct flag is required.")]
        public bool IsCorrect { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Marks must be a non-negative integer.")]
        public int? Marks { get; set; }

        // Navigation property
        public Question Question { get; set; }

        // Add this navigation property to fix the error
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
