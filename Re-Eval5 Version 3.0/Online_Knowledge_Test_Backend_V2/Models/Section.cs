using Online_Knowledge_Test_Backend_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }  // Integer-based ID

        [Required(ErrorMessage = "Exam ID is required.")]
        public int ExamId { get; set; }  // Foreign Key to Exam

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }  // Section title (e.g., Math, English)

        [Required(ErrorMessage = "Number of Questions is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of Questions must be a non-negative integer.")]
        public int NumberOfQuestions { get; set; }  // Number of questions in this section

        [Required(ErrorMessage = "Total Marks are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total Marks must be a non-negative integer.")]
        public int TotalMarks { get; set; }  // Total marks for this section

        [Required(ErrorMessage = "Passing Marks are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Passing Marks must be a non-negative integer.")]
        [LessThanOrEqualTo("TotalMarks", ErrorMessage = "Passing Marks must be less than or equal to Total Marks.")]
        public int passingMarks { get; set; }  // Passing marks for this section

        // Navigation properties
        public Exam Exam { get; set; }

        [Range(0.0, 100.0, ErrorMessage = "Weightage must be between 0 and 100.")]
        public decimal? Weightage { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<SectionResult> SectionResults { get; set; } = new List<SectionResult>();
    }


}
