using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        public int TotalQuestions { get; set; }

        [Required]
        public int AttemptedQuestions { get; set; }

        [Required]
        public int UnAttemptedQuestions { get; set; }

        [Required]
        public int CorrectQuestions { get; set; }

        [Required]
        public int InCorrectQuestions { get; set; }

        [Required, Range(0, 100)]
        public decimal PercentageObtained { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }

        public virtual User User { get; set; }
        public virtual Test Test { get; set; }
    }
}
