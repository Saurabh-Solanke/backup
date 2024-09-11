using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class FamilyDetailsDTO
    {
        public int FamilyDetailsId { get; set; }


        [Required(ErrorMessage = "Please enter the father name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string FathersFirstName { get; set; }


        [Required(ErrorMessage = "Please enter the father Surnamename")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string FathersLastName { get; set; }


        [Required(ErrorMessage = "Please enter the mother first name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string MothersFirstName { get; set; }


        [Required(ErrorMessage = "Please enter the mother Surnamename")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string MothersLastName { get; set; }


        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string? SpouceFirstName { get; set; }  // optional


        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string? SpouceLastName { get; set; }  // optional

        [Required]
        public bool IsMinor { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string? LeagalGuardianFirstName { get; set; }


        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name characters should have length between 3 to 50")]
        public string? LeagalGuardianLastName { get; set; }


        [StringLength(8, ErrorMessage = "Passport Number should be of Eight Characters")]
        public string? FatherPassportNo { get; set; }

        [StringLength(8, ErrorMessage = "Passport Number should be of Eight Characters")]
        public string? MotherPassportNo { get; set; }
    }
}
