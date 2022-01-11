using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class EnrollmentsController : ControllerBase
    {  
        public EnrollmentsController(IEnrollment enrollment,
        IMapper mapper)
        {
            _enrollment = enrollment;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult TestIndboundConnection(){
            Console.WriteLine("--> Inbound POST payment services");
            return Ok("Inbound test from enrollment controller");
        }
    }
}