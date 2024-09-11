using PassportApi.Models.Enums;
using PassportApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.Payment
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int ApplicationId { get; set; }


        [Required]
        public decimal ApplicationFee { get; set; }


        [Required]
        public ApplicationType ApplicationType { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public string? TransactionId { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
