using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; } // Enum: Teacher, Student
        [Required]
        public string MobileNo { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("ExamUser")]
        public string ExamUserId { get; set; }


        //Nav
        public ExamUser ExamUser { get; set; }

        public ICollection<Exam> CreatedExams { get; set; }

        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
