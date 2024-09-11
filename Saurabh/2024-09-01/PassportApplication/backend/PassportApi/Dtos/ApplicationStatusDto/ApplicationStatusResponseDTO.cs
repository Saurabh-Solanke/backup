namespace PassportApi.Dtos.ApplicationStatusDto
{
    public class ApplicationStatusResponseDTO
    {
        public int ApplicationId { get; set; }
        public string PassportNo { get; set; }
        public string ApplicantName { get; set; }

        public string ApplicationType { get; set; }

        public string PaymentStatus { get; set; }

        public string ApplicationStatus { get; set; }
    }
}
