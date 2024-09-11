using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Models
{
    public class StudentExam
    {
        [Key]
        public int StudentExamId { get; set; }

        [Required]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public string Status { get; set; } // Enum: NotStarted, InProgress, Completed

        [Required]
        public decimal? Score { get; set; }


        //Nav
        public Exam Exam { get; set; }

        public User Student { get; set; }

        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
