using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class SectionQuestion
    {
        [Key]
        public int SectionQuestionId { get; set; }

        [Required, ForeignKey("Section")]
        public int SectionId { get; set; }

        [Required, ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual Question Question { get; set; }
    }
}
