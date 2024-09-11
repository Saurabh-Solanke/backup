using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required, StringLength(100)]
        public string SectionName { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
