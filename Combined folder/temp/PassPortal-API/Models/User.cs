using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace PassportApplicationAPI.Models
{
    public class User : IdentityUser
    {
        //Extra fields that Identity doesnt provide but we need 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeRegion { get; set; }

        //Navigable Properties
        public ICollection<Payment> Payments { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
