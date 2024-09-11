using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class MasterDetailsTableDTO
    {
        public int ApplicationNo { get; set; }

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; } 

        public string? PassportNo { get; set; }

        [Required]
        public PassportType PassportType { get; set; }  // N/R

        [Required]
        public PaymentStatus PaymentStatus { get; set; }  // Enum for payment status

        public int? ServiceRequiredId { get; set; }


        public int? ApplicantDetailsTableID { get; set; }


        public int? FamilyDetailsId { get; set; }


        public int? AddressTableId { get; set; }

        public int? EmergencyContactDetailsId { get; set; }

        public int UserId { get; set; }
        public int? OtherDetailsId { get; set; }
        public int? DocumentTableId { get; set; }

        public DateTime? CreatedOn { get; set; }=DateTime.Now;

        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
