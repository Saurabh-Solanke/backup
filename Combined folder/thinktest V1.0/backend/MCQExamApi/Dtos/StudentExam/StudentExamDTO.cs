using MCQExamApi.Dtos.StudentAnswer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.StudentExam
{
    public class StudentExamDTO
    {
        public int? StudentExamId { get; set; }


        [Required(ErrorMessage = "ExamId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ExamId must be a positive integer.")]
        public int ExamId { get; set; }


        [Required(ErrorMessage = "StudentId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "StudentId must be a positive integer.")]
        public int StudentId { get; set; }


        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format for StartTime.")]
        public DateTime? StartTime { get; set; }


        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format for EndTime.")]
        public DateTime? EndTime { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("NotStarted|InProgress|Completed", ErrorMessage = "Status must be one of the following: 'NotStarted', 'InProgress', 'Completed'.")]
        public string Status { get; set; } // Enum: NotStarted, InProgress, Completed


        [Required(ErrorMessage = "Score is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Score must be a non-negative number.")]
        public decimal? Score { get; set; }

        public List<StudentAnswerDTO> Answers { get; set; }
    }
}
