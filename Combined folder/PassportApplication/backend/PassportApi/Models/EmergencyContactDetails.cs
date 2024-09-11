using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Models
{
    public class EmergencyContactDetails
    {
        [Key]
        public int EmergencyContactDetailsId { get; set; }


        [Required(ErrorMessage = "Please Enter the contact name.")]
        public string ContactName { get; set; }


        [Required(ErrorMessage = "Please Enter the contact mobile no.")]
        public string Mobile { get; set; }


        [Required(ErrorMessage = "Please Enter the contact email address.")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter the Address.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Please Enter the city.")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please Enter the state.")]
        public string State { get; set; }


        [Required(ErrorMessage = "Please Enter the pincode.")]
        public string Pincode { get; set; }


        [Required(ErrorMessage = "Please Enter the country.")]
        public string Country { get; set; }

    }
}
