using System.ComponentModel.DataAnnotations;

namespace PassportApplicationAPI.Models
{
    public class OtherDetails
    {
        public bool PendingPriceedings { get; set; }
        public string PendingProceedingCourtName { get; set; }
        public string PendingProceedingCaseNumber { get; set; }
        public string PendingProceedingLawSection { get; set; }


        public bool IsWarrentIssued { get; set; }
        public string WarrentIssuedCourtName { get; set; }
        public string WarrentIssuedCaseNumber { get; set; }
        public string WarrentIssuedLawSection { get; set; }


        public bool IsSummonned { get; set; }
        public string SummonCourtName { get; set; }
        public string SummonCaseNumber { get; set; }
        public string SummonLawSection { get; set; }


        public bool IsProhibitedFromDeparture { get; set; }
        public string ProhibitedFromDepartureCourtName { get; set; }
        public string ProhibitedFromDepartureCaseNumber { get; set; }
        public string ProhibitedFromDepartureLawSection { get; set; }


        public bool IsConvicted { get; set; }
        public string ConvictionCourtName { get; set; }
        public string ConvictionCaseNumber { get; set; }
        public DateTime ConvictionDate { get; set; }


        public bool HasBeenDeniedPassport { get; set; }
        public string DeniedPassportReason { get; set; }


        public bool HasPassportBeenImpounded { get; set; }
        public string ImpoundedPassportNumber { get; set; }
        public string PassportImpoundingReason { get; set; }
        

        public bool HasPassportBeenRevoked { get; set; }
        public string RevokedPassportNumber { get; set; }
        public string PassportRevokingReason { get; set; }


        public bool IsForeignCitizen { get; set; }
        public string CitizenCountryName { get; set; }


        public bool HadPassportFromOtherCountry { get; set; }
        public string OtherCountryCountryName { get; set; }


        public bool HasSurrenderedIndianPassport { get; set; }
        public string SurrenderedPassportNumber { get; set; }


        public bool HasAppliedForIndianCitizenshipRenunciation { get; set; }
        public string RenunciationApplicationNumber { get; set; }


        public bool HasReturnedOnEC { get; set; }
        public string ECNumber { get; set; }
        public DateTime ECIssueDate { get; set; }
        public string ECIssuingAuthority { get; set; }
        public DateTime ECReturnDate { get; set; }
        public string ECIssuingCountry {  get; set; }
        public string ECIssuingReason { get; set; }


        public bool HasBeenDeported {  get; set; }
        public string DepartionDetails { get; set; }


        public bool HasBeenRepatriatedBackToIndia { get; set; }
        public string RepatritionDetails { get; set; }
    }
}
