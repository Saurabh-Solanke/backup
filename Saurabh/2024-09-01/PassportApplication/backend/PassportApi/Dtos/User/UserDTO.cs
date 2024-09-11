using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Dtos.User
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNo { get; set; }

        public string PassportUserId { get; set; }


        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

        public string Role { get; set; }
    }
}
