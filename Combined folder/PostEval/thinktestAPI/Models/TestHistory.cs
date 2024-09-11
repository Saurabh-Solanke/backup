using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class TestHistory
    {
        [Key]
        public int TestHistoryId { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }

        [Required]
        public DateTime ConductedOn { get; set; }

        [Required, Range(0, 100)]
        public decimal FinalScore { get; set; }

        [Required]
        public bool Passed { get; set; }

        public virtual User User { get; set; }
        public virtual Test Test { get; set; }
    }
}
