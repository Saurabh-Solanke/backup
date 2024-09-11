using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.Auth
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string  UpdatedPassword { get; set; }
    }
}
