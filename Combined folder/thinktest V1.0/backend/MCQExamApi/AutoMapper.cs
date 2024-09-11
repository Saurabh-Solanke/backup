using AutoMapper;
using MCQExamApi.Dtos.Exam;
using MCQExamApi.Dtos.Option;
using MCQExamApi.Dtos.Question;
using MCQExamApi.Dtos.Result;
using MCQExamApi.Dtos.StudentAnswer;
using MCQExamApi.Dtos.StudentExam;
using MCQExamApi.Dtos.User;
using MCQExamApi.Models;
namespace MCQExamApi
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Exam, ExamDTO>().ReverseMap();
            CreateMap<Option,OptionDTO>().ReverseMap();
            CreateMap<Question,QuestionDTO>().ReverseMap();
            CreateMap<Result,ResultDTO>().ReverseMap(); 
            CreateMap<StudentAnswer,StudentAnswerDTO>().ReverseMap();
            CreateMap<StudentExam,StudentExamDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();

            CreateMap<Exam, ExamRespDTO>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

            CreateMap<Question, QuestionRespDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<Option, OptionRespDTO>();

            CreateMap<QuestionDTO, Question>()
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<Question, QuestionDTO>()
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
        }
    }
}
