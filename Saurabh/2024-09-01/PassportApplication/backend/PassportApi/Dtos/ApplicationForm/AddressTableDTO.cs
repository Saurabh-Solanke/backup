using PassportApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PassportApi.Dtos.ApplicationForm
{
    public class AddressTableDTO
    {
        public int AddressTableId { get; set; }


        [Required(ErrorMessage = "Please eneter the house details.")]
        public int THouseNo { get; set; }


        [Required(ErrorMessage = "Please enter the details of Street.")]
        public string TStreet { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public string TDistrict { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public string TPoliceStation { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public State TState { get; set; }  // Enum for state


        [Required(ErrorMessage = "Please enter the pincode.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Please enter 6 digit pincode")]
        public string TPincode { get; set; }


        [Required]
        public bool IsPermanent { get; set; }


        [Required(ErrorMessage = "Please eneter the house details.")]
        public int? HouseNo { get; set; }


        [Required(ErrorMessage = "Please enter the details of Street.")]
        public string Street { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public string District { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public string PoliceStation { get; set; }


        [Required(ErrorMessage = "Please Select one option.")]
        public State? State { get; set; }  // Enum for state


        [Required(ErrorMessage = "Please enter the pincode.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Please enter 6 digit pincode")]
        public string Pincode { get; set; }
    }
}
