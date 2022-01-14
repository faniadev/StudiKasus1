using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {
            CreateMap<Enrollment,EnrollmentReadDto>();
            CreateMap<PaymentCreateDto,Payment>();
            CreateMap<Payment,PaymentReadDto>()
            .ForMember(dest=>dest.TotalPrice,
            opt=>opt.MapFrom(src=>Convert.ToDecimal(src.CourseID * 500000)));
            CreateMap<EnrollmentPublishedDto,Enrollment>()
            .ForMember(dest=>dest.ExternalID,
            opt=>opt.MapFrom(src=>src.Id));
        }
    }
}