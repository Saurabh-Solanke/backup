using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class UserTestAnswer
    {
        [Key]
        public int UserTestAnswerId { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }

        [Required, ForeignKey("Question")]
        public int QuestionId { get; set; }

        [Required, ForeignKey("Option")]
        public int OptionId { get; set; }

        [Required]
        public bool IsSelected { get; set; }

        public virtual Test Test { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option Option { get; set; }
    }
}
