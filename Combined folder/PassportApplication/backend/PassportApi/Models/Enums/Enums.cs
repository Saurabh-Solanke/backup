namespace PassportApi.Models.Enums
{
    public enum ApplicationType
    {
        Normal = 1,
        Tatkaal = 2,
    }
    public enum PagesRequired
    {
        ThirtySix = 1,
        Sixty = 2,
    }
    public enum ValidityReq
    {
        TenYears = 1,
        EighteenYears = 2,
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3,
    }
    public enum MaritalStatus
    {
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widowed = 4,
        Separated=5,
    }
    public enum CitizenshipBy
    {
        Birth = 1,
        Descent = 2,
        Registration_Naturalization = 3,
    }
    public enum Education
    {
        SeventhPass=1,
        BetEightNine=2,
        TenthPass=3,
        GraduateAndAbove = 4,
    }
    public enum EmployeeType
    {
        PSU=1,
        Government = 2,
        StatutoryBody=3,
        RetiredGovServent = 4,
        SelfEmployed = 5,
        Private = 6,
        Homemaker= 7,
        NotEmployed= 8,
        RetiredPrivateService=9,
        Student= 10,
        Others= 11,
        OwnersPartnersDirectorsCllFICCIASSOCHAM=12,
    }
    public enum ApplicationStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }
    public enum PaymentStatus
    {
        NotPaid = 1,
        Paid = 2,
        Failed = 3,
        Pending = 4,
    }
    public enum State
    {
        AndhraPradesh = 1,
        ArunachalPradesh,
        Assam,
        Bihar,
        Chhattisgarh,
        Goa,
        Gujarat,
        Haryana,
        HimachalPradesh,
        Jharkhand,
        Karnataka,
        Kerala,
        MadhyaPradesh,
        Maharashtra,
        Manipur,
        Meghalaya,
        Mizoram,
        Nagaland,
        Odisha,
        Punjab,
        Rajasthan,
        Sikkim,
        TamilNadu,
        Telangana,
        Tripura,
        UttarPradesh,
        Uttarakhand,
        WestBengal,
        AndamanAndNicobarIslands,
        Chandigarh,
        DadraAndNagarHaveliAndDamanAndDiu,
        Lakshadweep,
        Delhi,
        Puducherry,
        Ladakh,
        JammuAndKashmir
    }


    public enum PassportType
    {
        New=1,
        Renewal=2,
    }

    public enum ChangeInAppearance {
        Appearance=1,
        Signature=2,
        GivenName=3,
        Surname=4,
        DOB=5,
        SpouseName=6,
        Address=7,
        DeleteECR=8,
        Other=9
    }

    public enum FeedbackComplaintType
    {
        Feedback=1,
        Complaint=2,
    }

    public enum ComplaintStatus
    { 
        Resolved= 1,
        Unresolved= 2,
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        BankTransfer,
    }

    public enum ReasonForRenewal { 
        ValidityExpiredWithinThreeYear=1,
        ValidityExpiredMoreThanThreeYear=2,
        ChangeInExisting=3,
        ExhaustionOfPages=4,
        LostPassport=5,
        DamagedPassport=6
    }

}
