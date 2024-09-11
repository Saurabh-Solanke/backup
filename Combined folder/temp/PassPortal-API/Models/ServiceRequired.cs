namespace PassportApplicationAPI.Models
{
    public class ServiceRequired
    {
        //These are not perfe4ct yet might need some finishing touches but this is the basic guiedline.
        public string ServiceType { get; set; }
        public bool IsTatkal { get; set; }
        public bool IsReissue { get; set; }
        public string ValidityYears { get; set; }
    }
}
