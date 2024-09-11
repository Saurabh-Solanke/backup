using System.ComponentModel.DataAnnotations;

namespace PassportApplicationAPI.Models
{
    public class CurrentAddress
    {
        [Required(ErrorMessage = "House Number is required.")]
        [StringLength(10, ErrorMessage = "House Number cannot exceed 10 characters.")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required.")]
        [StringLength(100, ErrorMessage = "Address Line 1 cannot exceed 100 characters.")]
        public string AddressLine1 { get; set; }

        [StringLength(100, ErrorMessage = "Address Line 2 cannot exceed 100 characters.")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Police Station is required.")]
        [StringLength(50, ErrorMessage = "Police Station cannot exceed 50 characters.")]
        public string PoliceStation { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "District is required.")]
        [StringLength(50, ErrorMessage = "District cannot exceed 50 characters.")]
        public string District { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; } = "India";

        [Required(ErrorMessage = "Zip Code is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid Zip Code. Zip Code must be 6 digits.")]
        public string ZipCode { get; set; }
    }
}
}
