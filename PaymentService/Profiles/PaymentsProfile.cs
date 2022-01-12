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
            CreateMap<Payment,PaymentReadDto>();
            CreateMap<EnrollmentPublishedDto,Enrollment>()
            .ForMember(dest=>dest.ExternalID,
            opt=>opt.MapFrom(src=>src.Id));
        }
    }
}