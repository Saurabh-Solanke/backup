using AutoMapper;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.Mappings
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Result, ExamResultDto>().ReverseMap();
            CreateMap<User, LoggedInUser>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<Result, ResultDataDto>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Result, UserResultDto>().ReverseMap();
            CreateMap<Subject, GetSubjectDto>().ReverseMap();
            CreateMap<Result, UserResultDto>().ReverseMap();

            CreateMap<Test, CreateTestDto>().ReverseMap();
            CreateMap<UserTestAnswer, UserTestAnswerDto>().ReverseMap();
        }
    }
}
