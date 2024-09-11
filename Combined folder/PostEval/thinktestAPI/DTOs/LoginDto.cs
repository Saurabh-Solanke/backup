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
        [Required(ErrorMessage ="Please enter the email.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage ="Please enter the password.")]
        public string Password { get; set; }
    }
}
