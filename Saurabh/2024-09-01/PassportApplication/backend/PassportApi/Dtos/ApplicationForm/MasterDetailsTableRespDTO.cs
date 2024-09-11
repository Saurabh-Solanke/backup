using PassportApi.Models.Enums;

namespace PassportApi.Dtos.ApplicationForm
{
    public class MasterDetailsTableRespDTO
    {
        public int ApplicationNo { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public PassportType PassportType { get; set; }  // N/R

        public int UserId { get; set; }

        public string? ApplicantName { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;

    }
}
