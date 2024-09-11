using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PassportApi.Models.Enums;

namespace PassportApi.Models
{
    public class MasterDetailsTable
    {
        [Key]
        public int ApplicationNo { get; set; }


        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }  // enum

        public string? PassportNo { get; set; }

        [Required]
        public PassportType PassportType { get; set; }  // N/R

        [Required]
        public PaymentStatus PaymentStatus { get; set; }  // enum

        [ForeignKey("ServiceRequired")]
        public int? ServiceRequiredId { get; set; }
        public ServiceRequired ServiceRequired { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("ApplicantDetails")]
        public int? ApplicantDetailsTableID { get; set; }
        public ApplicantDetails ApplicantDetails { get; set; }



        [ForeignKey("FamilyDetails")]
        public int? FamilyDetailsId { get; set; }
        public FamilyDetails FamilyDetails { get; set; }



        [ForeignKey("AddressTable")]
        public int? AddressTableId { get; set; }
        public AddressTable AddressTable { get; set; }



        [ForeignKey("EmergencyContactDetails")]
        public int? EmergencyContactDetailsId { get; set; }
        public EmergencyContactDetails EmergencyContactDetails { get; set; }



        [ForeignKey("OtherDetails")]
        public int? OtherDetailsId { get; set; }
        public OtherDetails OtherDetails { get; set; }



        [ForeignKey("DocumentTable")]
        public int? DocumentTableId { get; set; }
        public DocumentTable DocumentTable { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Payment> Payments { get; set; }
    }
}
