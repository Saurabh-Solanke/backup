using System.ComponentModel.DataAnnotations;

namespace PassPortal_API.Models
{
    public class PassportOffice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OfficeName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
