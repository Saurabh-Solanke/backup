using Online_Knowledge_Test_Backend_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }  // Integer-based ID

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        public string CreatedByUserId { get; set; }  // Foreign Key to ApplicationUser

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.DateTime)]
        [GreaterThan("StartDate", ErrorMessage = "End Date must be greater than Start Date.")]
        public DateTime EndDate { get; set; }

        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int Duration { get; set; }  // Duration in minutes

        [Required(ErrorMessage = "Total Marks are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total Marks must be a non-negative integer.")]
        public int TotalMarks { get; set; }

        [Required(ErrorMessage = "Passing Marks are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Passing Marks must be a non-negative integer.")]
        [LessThanOrEqualTo("TotalMarks", ErrorMessage = "Passing Marks must be less than or equal to Total Marks.")]
        public int PassingMarks { get; set; }

        public bool isRandmized { get; set; }

        // Navigation properties
        public User CreatedByUser { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
    }

    // Custom validation attributes for comparisons
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public GreaterThanAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_propertyName);
            if (otherProperty == null)
                return new ValidationResult($"Property with name {_propertyName} not found.");

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (value is DateTime dateValue && otherValue is DateTime otherDateValue)
            {
                if (dateValue <= otherDateValue)
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class LessThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public LessThanOrEqualToAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_propertyName);
            if (otherProperty == null)
                return new ValidationResult($"Property with name {_propertyName} not found.");

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (value is int intValue && otherValue is int otherIntValue)
            {
                if (intValue > otherIntValue)
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
