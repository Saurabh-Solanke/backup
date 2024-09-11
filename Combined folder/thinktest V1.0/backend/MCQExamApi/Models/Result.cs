using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        [ForeignKey("StudentExam")]
        public int StudentExamId { get; set; }

        [Required]
        public decimal TotalScore { get; set; }

        [Required]
        public decimal ObtainedScore { get; set; }

        //Nav
        public StudentExam StudentExam { get; set; }
    }
}
