using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PassportApi.Models.Enums;

namespace PassportApi.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("MasterDetailsTable")]
        public int ApplicationId { get; set; }

        [Required]
        public decimal ApplicationFee { get; set; }

        [Required]
        public ApplicationType ApplicationType { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentMethod? PaymentMethod { get; set; } 
        public string? TransactionId { get; set; } 

        public PaymentStatus PaymentStatus { get; set; }

        public MasterDetailsTable Application { get; set; }

    }
}
