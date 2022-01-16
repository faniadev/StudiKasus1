using System;
using AutoMapper;

namespace EnrollmentServices.Profiles
{
    public class EnrollmentsProfile : Profile
    {
        public EnrollmentsProfile()
        {
            CreateMap<Models.Enrollment, Dtos.EnrollmentDto>();
            CreateMap<Dtos.EnrollmentForCreateDto, Models.Enrollment>();
            CreateMap<Models.Enrollment, Dtos.EnrollmentForCreateDto>();
        }
    }
}
