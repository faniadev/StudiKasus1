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
            CreateMap<PaymentCreateDto,Payment>();
            CreateMap<Payment,PaymentReadDto>()
            .ForMember(dest=>dest.TotalPrice,
            opt=>opt.MapFrom(src=>Convert.ToDecimal(src.PaymentID * 500000)));
        }
    }
}