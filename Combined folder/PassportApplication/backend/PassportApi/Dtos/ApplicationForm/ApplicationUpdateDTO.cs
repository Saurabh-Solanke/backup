using PassportApi.Models.Enums;

namespace PassportApi.Dtos.ApplicationForm
{
    public class ApplicationUpdateDTO
    {
        public int ApplicationNo { get; set; }

        public int UserId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime createdOn { get; set; }
        public PassportType PassportType { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
