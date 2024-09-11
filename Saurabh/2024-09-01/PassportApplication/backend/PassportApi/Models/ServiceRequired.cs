using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PassportApi.Models.Enums;
namespace PassportApi.Models
{
    public class ServiceRequired
    {
        [Key]
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
