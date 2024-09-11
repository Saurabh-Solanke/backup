using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.Exam
{
    public class ExamDTO
    {
        public int ExamId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "CreatedBy is required.")]
        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "StartDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format for StartDate.")]
        [CustomValidation(typeof(ExamDTO), nameof(ValidateStartDate))]
        public DateTime StartDate { get; set; }


        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format for EndDate.")]
        [CustomValidation(typeof(ExamDTO), nameof(ValidateEndDate))]
        public DateTime? EndDate { get; set; }


        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int Duration { get; set; } // In Minutes


        [Range(1, int.MaxValue, ErrorMessage = "TotalMarks must be a positive number.")]
        public int? TotalMarks { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "PassingMarks must be a positive number.")]
        public int? PassingMarks { get; set; }



        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }



        // Custom validation for StartDate
        public static ValidationResult ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate < DateTime.Now)
            {
                return new ValidationResult("StartDate cannot be in the past.");
            }
            return ValidationResult.Success;
        }

        // Custom validation for EndDate
        public static ValidationResult ValidateEndDate(DateTime? endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as ExamDTO;
            if (endDate.HasValue && endDate <= instance.StartDate)
            {
                return new ValidationResult("EndDate must be after StartDate.");
            }
            return ValidationResult.Success;
        }
    }
}
