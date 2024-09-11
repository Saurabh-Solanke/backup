using System.Runtime.CompilerServices;

namespace PassportApplicationAPI.Models
{
    public class Application
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        public string FormId { get; set; }
        public bool IsNew { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PassportNumber { get; set; }
        public User User { get; set; }
        public Form Form { get; set; }
        public string RenewalReason { get; set; }
        public string isChangeInPersonalParticulars { get; set; }
        public string ChangeInPersonalParticulars { get; set; }
    }
}
