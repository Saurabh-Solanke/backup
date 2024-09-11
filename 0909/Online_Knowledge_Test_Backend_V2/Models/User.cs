using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class User: IdentityUser
    {

        [Required(ErrorMessage = "Please enter the name.")]
        [StringLength(100, ErrorMessage = "Name should be less than 100 characters long.")]
        public string Fullname { get; set; }


        [Required(ErrorMessage = "Please enter mobile no.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit Mobile no.")]
        public string MobileNo { get; set; }


        public bool IsActive { get; set; } = true;


        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
