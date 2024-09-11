export enum ApplicationType {
    Normal = 1,
    Tatkal = 2,
}

export enum PagesRequired {
    ThirtySix = 1,
    Sixty = 2,
}

export enum ValidityReq {
    TenYears = 1,
    EighteenYears = 2,
}

export enum Gender {
    Male = 1,
    Female = 2,
    Other = 3,
}

export enum MaritalStatus {
    Single = 1,
    Married = 2,
    Divorced = 3,
    Widowed = 4,
    Separated = 5,
}

export enum CitizenshipBy {
    Birth = 1,
    Descent = 2,
    Registration_Naturalization = 3,
}

export enum Education {
    SeventhPass = 1,
    BetEightNine = 2,
    TenthPass = 3,
    GraduateAndAbove = 4,
}

export enum EmployeeType {
    PSU = 1,
    Government = 2,
    StatutoryBody = 3,
    RetiredGovServent = 4,
    SelfEmployed = 5,
    Private = 6,
    Homemaker = 7,
    NotEmployed = 8,
    RetiredPrivateService = 9,
    Student = 10,
    Others = 11,
    OwnersPartnersDirectorsCllFICCIASSOCHAM = 12,
}

export enum ApplicationStatus {
    Pending = 1,
    Approved = 2,
    Rejected = 3,
}

export enum PaymentStatus {
    NotPaid = 1,
    Paid = 2,
    Failed = 3,
    Pending = 4,
}

export enum StateEnum {
    'Andhra Pradesh' = 1,
    'Arunachal Pradesh',
    'Assam',
    'Bihar',
    'Chhattisgarh',
    'Goa',
    'Gujarat',
    'Haryana',
    'Himachal Pradesh',
    'Jharkhand',
    'Karnataka',
    'Kerala',
    'Madhya Pradesh',
    'Maharashtra',
    'Manipur',
    'Meghalaya',
    'Mizoram',
    'Nagaland',
    'Odisha',
    'Punjab',
    'Rajasthan',
    'Sikkim',
    'Tamil Nadu',
    'Telangana',
    'Tripura',
    'Uttar Pradesh',
    'Uttarakhand',
    'West Bengal',
    'Andaman and Nicobar Islands',
    'Chandigarh',
    'Dadra and Nagar Haveli and Daman and Diu',
    'Lakshadweep',
    'Delhi',
    'Puducherry',
    'Ladakh',
    'Jammu and Kashmir',
  }
  

export enum PassportType {
    New = 1,
    Renewal = 2,
}

export enum ChangeInAppearance {
    Appearance = 1,
    Signature = 2,
    GivenName = 3,
    Surname = 4,
    DOB = 5,
    SpouseName = 6,
    Address = 7,
    DeleteECR = 8,
    Other = 9,
}

export enum FeedbackComplaintType {
    Feedback = 1,
    Complaint = 2,
}

export enum ComplaintStatus {
    Resolved = 1,
    Unresolved = 2,
}

export enum PaymentMethod {
    CreditCard = 1,
    DebitCard = 2,
    BankTransfer = 3,
}

export enum ReasonForRenewal {
    ValidityExpiredWithinThreeYear = 1,
    ValidityExpiredMoreThanThreeYear = 2,
    ChangeInExisting = 3,
    ExhaustionOfPages = 4,
    LostPassport = 5,
    DamagedPassport = 6,
}
