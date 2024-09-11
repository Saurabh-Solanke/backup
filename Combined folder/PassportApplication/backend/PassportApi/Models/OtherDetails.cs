using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Models
{
    public class OtherDetails
    {
        [Key]
        public int OtherDetailsId { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool CriminalConvictions { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool RefusedPassport { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool ImpoundedPassport { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool RevokedPassport { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool GrantedCitizenship { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool HeldForeignPassport { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool SurrenderedIndianPassport { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool AppliedRenunciation { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool PassportSurrendered { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool? Renunciation { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool EmergencyCertificate { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool Deported { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool Repatriated { get; set; }

        [Required(ErrorMessage = "Please choose one option.")]
        public bool RegisteredMission { get; set; }

        [StringLength(255)]
        public string? RegisteredMissionName { get; set; }

    }
}
