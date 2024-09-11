using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Formatters;
using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Section ID is required.")]
        public int SectionId { get; set; }

        [Required(ErrorMessage = "Question Text is required.")]
        [StringLength(1000, ErrorMessage = "Question Text cannot exceed 1000 characters.")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Is Multiple Choice flag is required.")]
        public bool IsMultipleChoice { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public Section Section { get; set; }

        public bool? HasDifferentialMarking { get; set; }

        [EnumDataType(typeof(MediaType), ErrorMessage = "Invalid Media Type.")]
        public MediaType? mediaType { get; set; }

        [StringLength(500, ErrorMessage = "Media URL cannot exceed 500 characters.")]
        public string? MediaUrl { get; set; }

        public ICollection<Option> Options { get; set; } = new List<Option>();

        // Navigation property to fix the error
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();


        // Nullable foreign key for QuestionBank
        public int? QuestionBankId { get; set; }

        // Navigation property for QuestionBank
        public QuestionBank? QuestionBank { get; set; }
    }

  
}
