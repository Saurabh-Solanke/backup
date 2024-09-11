using Online_Knowledge_Test_Backend_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class ExamResult
    {
        [Key]
        public int ExamResultId { get; set; }  // Integer-based ID

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }  // Foreign Key to ApplicationUser

        [Required(ErrorMessage = "Exam ID is required.")]
        public int ExamId { get; set; }  // Foreign Key to Exam

        [Required(ErrorMessage = "Attempt Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Attempt Number must be greater than 0.")]
        public int AttemptNumber { get; set; }

        [Required(ErrorMessage = "Total Score is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total Score must be a non-negative integer.")]
        public int TotalScore { get; set; }

        [Required(ErrorMessage = "Percentage is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Percentage must be between 0 and 100.")]
        public double Percentage { get; set; }

        [Required(ErrorMessage = "Passed status is required.")]
        public bool Passed { get; set; }

        [Required(ErrorMessage = "Completed Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CompletedDate { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a non-negative integer.")]
        public int Duration { get; set; }  // Duration in minutes

        [Range(0, int.MaxValue, ErrorMessage = "Mark for Review must be a non-negative integer.")]
        public int markforreview { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Exam Exam { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<SectionResult> SectionResults { get; set; }
    }
}
