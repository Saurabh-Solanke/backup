using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCQExamApi.Dtos.User
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("Teacher|Student", ErrorMessage = "Role must be either 'Teacher' or 'Student'.")]
        public string Role { get; set; } // Enum: Teacher, Student

        [Required(ErrorMessage = "MobileNo is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "MobileNo cannot be longer than 15 digits.")]
        public string MobileNo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ExamUserId is required.")]
        [RegularExpression(@"^[a-fA-F0-9\-]{36}$", ErrorMessage = "ExamUserId must be a valid GUID format.")]
        public string ExamUserId { get; set; } // Assuming it's a GUID stored as string

    }
}
