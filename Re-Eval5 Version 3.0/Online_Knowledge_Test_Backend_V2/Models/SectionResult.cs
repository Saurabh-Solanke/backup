using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class SectionResult
    {
        [Key]
        public int SectionResultId { get; set; }

        [Required(ErrorMessage = "Section ID is required.")]
        public int SectionId { get; set; }  // Foreign Key to Section

        [Required(ErrorMessage = "Exam Result ID is required.")]
        public int ExamResultId { get; set; }  // Foreign Key to ExamResult

        [Required(ErrorMessage = "Attempted Questions are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Attempted Questions must be a non-negative integer.")]
        public int AttemptedQuestions { get; set; }

        [Required(ErrorMessage = "Correct Answers are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Correct Answers must be a non-negative integer.")]
        public int CorrectAnswers { get; set; }

        [Required(ErrorMessage = "Section Score is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Section Score must be a non-negative number.")]
        public double SectionScore { get; set; }

        [Required(ErrorMessage = "Is Passed flag is required.")]
        public bool IsPassed { get; set; }

        // Navigation properties
        public Section Section { get; set; }
        public ExamResult ExamResult { get; set; }
    }
}
