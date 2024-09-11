namespace PassportApplicationAPI.Models
{
    public class ApplicantDetails
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public bool IsKnownByOtherName { get; set; }
        public string OtherName { get; set; }
        public bool HasAnAlias { get; set; }
        public string AliasName { get; set; }
        public string AadhaarCardNumber { get; set; }
        public bool HasChangedName { get; set; }
        public string PreviousName { get; set; }
        public string Citizenship { get; set; }
        public string DistinguishingMark { get; set; }
        public string District { get; set; }
        public DateTime Dob { get; set; }
        public string Education { get; set; }
        public string EmploymentType { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public bool IsEligibleForNonECR { get; set; }
        public string OrganizationName { get; set; }
        public string PanCardNumber { get; set; }
        public bool IsParentOrSpouseGovernmentServant { get; set; }
        public string PlaceOfBirth { get; set; }
        public string RegionCountry { get; set; }
        public string State { get; set; }
        

    }
}
