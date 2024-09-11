using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportApi.Models
{
    public class DocumentTable
    {
        [Key]
        public int DocumentTableId { get; set; }


        [Required(ErrorMessage = "Please upload the Aadharcard.")]
        public byte[] AadharCard { get; set; }


        [Required(ErrorMessage = "Please upload the Passport Size Photo.")]
        public byte[] Photo { get; set; }


        [Required(ErrorMessage = "Please upload the Signature Image.")]
        public byte[] Signature { get; set; }

        public byte[]? Pancard { get; set; }

        public byte[]? RecentPassport { get; set; }
    }
}
