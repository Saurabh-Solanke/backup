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
        public string UserFullname { get; set; }

        [Required(ErrorMessage = "Please enter the email.")]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Please enter mobile no.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit Mobile no.")]
        public string UserMobileNo { get; set; }

        [Required(ErrorMessage = "Please enter the address.")]
        [StringLength(100, ErrorMessage = "Address character length should be less than 100.")]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "Please enter the Pincode.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Please enter the 6 Digit Pincode.")]
        public string UserPincode { get; set; }


    }
}
