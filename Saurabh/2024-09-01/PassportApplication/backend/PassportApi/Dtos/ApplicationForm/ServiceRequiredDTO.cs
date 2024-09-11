using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class ServiceRequiredDTO
    {
        public int ServiceRequiredId { get; set; }

        [Required(ErrorMessage = "Please select one option")]
        public ApplicationType ApplicationType { get; set; }

        [Required(ErrorMessage = "Please select one option")]
        public PagesRequired PagesRequired { get; set; }

        [Required(ErrorMessage = "Please select one option")]
        public ValidityReq ValidityReq { get; set; }

        public ReasonForRenewal? ReasonForRenewal { get; set; }

        public ChangeInAppearance? ChangeInAppearance { get; set; }
    }
}
