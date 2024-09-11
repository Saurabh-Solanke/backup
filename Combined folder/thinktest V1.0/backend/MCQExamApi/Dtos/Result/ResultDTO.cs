using MCQExamApi.Dtos.Exam;
using MCQExamApi.Dtos.StudentExam;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.Result
{
    public class ResultDTO
    {
        public int ResultId { get; set; }

        [Required(ErrorMessage = "StudentExamId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "StudentExamId must be a positive integer.")]
        public int StudentExamId { get; set; }


        [Required(ErrorMessage = "TotalScore is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "TotalScore must be a positive number.")]
        public decimal TotalScore { get; set; }


        [Required(ErrorMessage = "ObtainedScore is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "ObtainedScore must be a non-negative number.")]
        public decimal ObtainedScore { get; set; }


    }
}
