using AutoMapper;
using DemoWebAPIWithPostgres.Dto;
using DemoWebAPIWithPostgres.Models;

namespace DemoWebAPIWithPostgres.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, AddStudentDto>();
            CreateMap<AddStudentDto, Student>();

        }
    }
}
