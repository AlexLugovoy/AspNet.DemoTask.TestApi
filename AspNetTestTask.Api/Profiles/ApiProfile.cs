using AspNetTestTask.Api.DTO;
using AspNetTestTask.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentity.Api.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<RegisterUserDTO, IdentityUser>().ForMember(e=>e.UserName, opt=>opt.MapFrom(src=>src.Email));
            CreateMap<LoginDTO, IdentityUser>();
            CreateMap<Test, TestReadDTO>();
            CreateMap<Question, QuestionReadDTO>();
            CreateMap<Answer, AnswerReadDTO>();
            CreateMap<AnswerReadDTO, Answer>();
        }
    }
}
