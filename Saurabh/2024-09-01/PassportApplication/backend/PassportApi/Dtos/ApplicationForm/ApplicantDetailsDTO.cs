using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class ApplicantDetailsDTO
    {
        public int ApplicantDetailsTableID { get; set; }


        [Required(ErrorMessage = "Please enter the Applicant first name")]
        [StringLength(50, ErrorMessage = "name characters length should be between 3 to 50")]
        public string ApplicantFirstName { get; set; }



        [Required(ErrorMessage = "Please enter the Applicant surname")]
        [StringLength(50, ErrorMessage = "Name characters length should be between 3 to 50")]
        public string ApplicantLastName { get; set; }



        [Required(ErrorMessage = "Please enter the email.")]
        [EmailAddress]
        [StringLength(255)]
        public string ApplicantEmail { get; set; }


        [Required(ErrorMessage = "Please enter the Mobile No")]
        public string MobileNo { get; set; }


        [Required(ErrorMessage = "Please enter the Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public Gender Gender { get; set; }  // Enum for gender


        [Required(ErrorMessage = "Please enter the place of birth.")]
        public string PlaceOfBirth { get; set; }


        [Required(ErrorMessage = "Please enter the District.")]
        public string? District { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public string? State { get; set; }


        [Required(ErrorMessage = "Please enter the Country.")]
        public string? Country { get; set; }

        public string? Pancard { get; set; }


        [Required(ErrorMessage = "Please Enter the aadhar card number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Please enter the valid number.")]
        public string Aadharcard { get; set; }

        public string? VoterId { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public MaritalStatus MaritialStatus { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public CitizenshipBy Citizenship { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public Education Education { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public EmployeeType EmployeeType { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public bool GovermentServent { get; set; }

        public string? OrganizationalName { get; set; }


        [Required(ErrorMessage = "Please choose the option.")]
        public bool NonECR { get; set; }


        [Required(ErrorMessage = "Please enter the Distinguish mark")]
        public string DistinguishMark { get; set; }


        [Required(ErrorMessage = "Please choose option.")]
        public bool NameChanged { get; set; }

        public string? ChangedName { get; set; }


        [Required(ErrorMessage = "Please choose option.")]
        public bool Alias { get; set; }

        public string? AliasName { get; set; }

        public string? PassportNo { get; set; }

        public DateTime? DateOfIssue { get; set; }

    }
}
