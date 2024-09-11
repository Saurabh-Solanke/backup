using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Please enter the name.")]
        [StringLength(100, ErrorMessage = "Name should be less than 100 characters long.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter the email.")]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter mobile no.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit Mobile no.")]
        public string MobileNo { get; set; }

    }
}
