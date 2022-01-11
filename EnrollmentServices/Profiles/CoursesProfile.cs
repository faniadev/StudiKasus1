using System;
using AutoMapper;
namespace EnrollmentServices.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Models.Course, Dtos.CourseDto>();
            CreateMap<Dtos.CourseForCreateDto, Models.Course>();
        }
    }
}
