namespace PassportApplicationAPI.Models
{
    public class AddressDetails
    {

        //Here i have two different addresses that are also deconstructed in the database. 

        //The fields in thwse are same but when going to teh database theywill be represented as : 
        //
        public bool IsAddressSame { get; set; }
        public CurrentAddress CurrentAddress { get; set; }
        public PermanentAddress PermanentAddress { get; set; }
    }
}
