using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class PassingThreshold
    {
        [Key]
        public int ThresholdId { get; set; }

        [Required, ForeignKey("Section")]
        public int SectionId { get; set; }

        [Required, Range(0, 100)]
        public decimal ThresholdPercentage { get; set; }

        public virtual Section Section { get; set; }
    }
}
