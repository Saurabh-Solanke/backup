using AutoMapper;
using PassportApi.Dtos.ApplicationForm;
using PassportApi.Dtos.Payment;
using PassportApi.Dtos.User;
using PassportApi.Models;

namespace PassportApi
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {

            CreateMap<ServiceRequired,ServiceRequiredDTO>().ReverseMap();
            CreateMap<AddressTable, AddressTableDTO>().ReverseMap();
            CreateMap<ApplicantDetails,ApplicantDetailsDTO>().ReverseMap();
            CreateMap<DocumentTable, DocumentTableDTO>().ReverseMap();
            CreateMap<EmergencyContactDetails, EmergencyContactDetailsDTO>().ReverseMap();
            CreateMap<FamilyDetails, FamilyDetailsDTO>().ReverseMap();
            CreateMap<FeedbackComplaint, FeedbackComplaintDTO>().ReverseMap();
            CreateMap<MasterDetailsTable, MasterDetailsTableDTO>().ReverseMap();
            CreateMap<OtherDetails, OtherDetailsDTO>().ReverseMap();
            CreateMap<Payment,PaymentDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
        }
    }
}
