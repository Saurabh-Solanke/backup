using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please enter the email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }
    }
}
