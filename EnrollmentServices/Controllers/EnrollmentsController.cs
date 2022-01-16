using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrollmentServices.Data;
using EnrollmentServices.Dtos;
using EnrollmentServices.Models;
using EnrollmentServices.SyncDataServices.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using EnrollmentServices.Helpers;
using System.Text;

namespace EnrollmentServices.Controllers
{
    [Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper, IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory  )
        {
            _enrollment = enrollment;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> GetAllEnrollment()
        {
            Console.WriteLine("--> Getting Enrollments .....");
            var results = await _enrollment.GetAll();
            //return results;
            return results;
            
            
        }

        // GET api/<EnrollmentsController>/5

        [Authorize(Roles = "admin,student")]
        [HttpGet("{id}", Name = "GetEnrollmentById")]
        public ActionResult<EnrollmentDto> GetEnrollmentById(int id)
        {
            var result = _enrollment.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<EnrollmentDto>(result));
        }

        // POST api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
            var enrollmentModel = _mapper.Map<Enrollment>(enrollmentForCreateDto);
            var result = await _enrollment.Insert(enrollmentModel);
            if (result != null)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
          clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

          using (var client = new HttpClient(clientHandler))
          {
            string token = Request.Headers["Authorization"];
            string[] tokenWords = token.Split(' ');
            var payment = new PaymentCreateDto
            {
              CourseID = result.CourseID,
              StudentID = result.StudentID,
              EnrollmentID = result.EnrollmentID
              
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenWords[1]);
            var json = JsonSerializer.Serialize(payment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_appSettings.PaymentService + "/api/p/Payments", data);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Payment Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Payment Service failed");
            }
          }

        }
        return Ok(_mapper.Map<EnrollmentForCreateDto>(result));
      }
      catch (System.Exception ex)
      {
        Console.WriteLine(ex);
        return BadRequest(ex.Message);
      }

        } 
    }
}
