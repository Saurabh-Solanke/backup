using AutoMapper;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;

namespace Online_Knowledge_Test_Backend_V2.Mappings
{
    public class AutoMapperClass: Profile
    {
        public AutoMapperClass()
        {
            // CreateMap<Source, Destination>();

            CreateMap<User, LoggedInUser>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
        }

    }
}
