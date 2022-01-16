using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/Payments")]
    public class PaymentsController : ControllerBase
    {
       private readonly IPaymentRepo<Payment> _repository;
            private readonly IMapper _mapper;
            public PaymentsController(IPaymentRepo<Payment> repository,
            IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            [HttpGet]
            public async Task<ActionResult<PaymentReadDto>> GetPaymentsForEnrollment()
            {
                var results = await _repository.GetPaymentsForEnrollment();
                return Ok(_mapper.Map<IEnumerable<PaymentReadDto>>(results));
            }

            [HttpGet("{paymentId}", Name = "GetPaymentForEnrollment")]
            public async Task <ActionResult<PaymentReadDto>> GetPaymentByID(int id)
            {
                try
                {
                    var results = await _repository.GetPaymentByID(id);
                    if (results == null)
                    return NotFound();
                    return Ok(_mapper.Map<PaymentReadDto>(results));
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            public async Task<ActionResult<PaymentReadDto>> CreatePayment(PaymentCreateDto paymentCreateDto)
            {
                try
                {
                    var paymentdtos = _mapper.Map<Payment>(paymentCreateDto);
                    var result = await _repository.CreatePayment(paymentdtos);
                    return Ok(_mapper.Map<PaymentReadDto>(result));
                }
                catch (System.Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            } 
    }
}