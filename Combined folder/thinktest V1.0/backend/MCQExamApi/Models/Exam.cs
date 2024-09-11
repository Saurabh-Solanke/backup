using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("CreatedBy")]
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int Duration { get; set; } // In Minutes

        public int? TotalMarks { get; set; }
        public int? PassingMarks { get; set; }

        [Required]
        public bool IsActive { get; set; }



        //Nav
        public User Creator { get; set; }//Teacher

        public ICollection<Question> Questions { get; set; }

        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
