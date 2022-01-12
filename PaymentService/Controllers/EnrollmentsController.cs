using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepo _repository;

        public EnrollmentsController(IPaymentRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<EnrollmentReadDto>> GetEnrollments()
        // {
        //     Console.WriteLine("-->Ambil Enrollments dari PaymentsService");
        //     var enrollmentItems = _repository.GetAllEnrollments();
        //     return Ok(_mapper.Map<IEnumerable<EnrollmentReadDto>>(enrollmentItems));
        // }

        [HttpPost]
        public ActionResult TestIndboundConnection(){
            Console.WriteLine("--> Inbound POST payment services");
            return Ok("Inbound test from enrollment controller");
        }
    }
}