using PassportApi.Models.Enums;

namespace PassportApi.Dtos.ApplicationForm
{
    public class ServiceRequiredResponseDTO
    {
        public int ServiceRequiredId { get; set; }

        public ApplicationType ApplicationType { get; set; }

        public PagesRequired PagesRequired { get; set; }

        public ValidityReq ValidityReq { get; set; }

        public ReasonForRenewal? ReasonForRenewal { get; set; }

        public ChangeInAppearance? ChangeInAppearance { get; set; }
    }
}
