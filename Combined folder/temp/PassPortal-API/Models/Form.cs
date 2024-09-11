using static System.Net.Mime.MediaTypeNames;

namespace PassportApplicationAPI.Models
{
    public class Form
    {
        public string FormId { get; set; }

        //these are objects that will be deconstructed in the database and hence there will be only one table foe the form in the db and we can handle the entities more easily in the backend part. using these objects.
        public ServiceRequired ServiceRequired { get; set; }

        //these are the details of the person for whom the passporet is being issued.
        public ApplicantDetails ApplicantDetails { get; set; }
        public FamilyDetails FamilyDetails { get; set; }

        //this is the address details objects 
        public AddressDetails AddressDetails { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public OtherDetails OtherDetails { get; set; }
        public SelfDeclaration SelfDeclaration { get; set; }
    }
}
