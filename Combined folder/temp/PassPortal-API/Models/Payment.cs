using static System.Net.Mime.MediaTypeNames;

namespace PassportApplicationAPI.Models
{
    public class Payment
    {
        public string Id { get; set; }              // Primary Key
        public string Amount { get; set; }          // Payment Amount
        public string PaymentMode { get; set; }     // Mode of Payment (e.g., Credit Card, PayPal, etc.)
        public string PaymentTransactionId { get; set; }
        public string PaymentStatus { get; set; }   // Status of the Payment (e.g., Completed, Pending, etc.)
        public DateTime PaymentDate { get; set; }   // Date of the Payment
        public string PaymentRemarks { get; set; }  // Any additional remarks or comments about the payment


        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        // Foreign key to User table
        public string UserId { get; set; }          // Foreign Key to the User table
        public User User { get; set; }
    }
}
