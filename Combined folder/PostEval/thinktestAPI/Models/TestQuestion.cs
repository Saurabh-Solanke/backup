using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class TestQuestion
    {
        [Key]
        public int TestQuestionId { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }

        [Required, ForeignKey("Section")]
        public int SectionId { get; set; }

        [Required, ForeignKey("Question")]
        public int QuestionId { get; set; }

        public bool IsAnswered { get; set; } = false;
        public bool IsMarkedForReview { get; set; } = false;
        public bool IsCurrent { get; set; } = false;

        public virtual Test Test { get; set; }
        public virtual Section Section { get; set; }
        public virtual Question Question { get; set; }
    }
}
