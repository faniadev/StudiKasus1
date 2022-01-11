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

namespace EnrollmentServices.Controllers
{
    [Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private IMapper _mapper;
        private readonly IPaymentDataClient _paymentDataClient;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper, IPaymentDataClient paymentDataClient )
        {
            _enrollment = enrollment;
            _mapper = mapper;
            _paymentDataClient = paymentDataClient;
        }

        // GET: api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetAllEnrollment()
        {
            Console.WriteLine("--> Getting Enrollments .....");
            var results = _enrollment.GetAllEnrollment();
            //return results;
            return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(results));
            
            
        }

        // GET api/<EnrollmentsController>/5

        [Authorize(Roles = "admin,student")]
        [HttpGet("{id}", Name = "GetEnrollmentById")]
        public ActionResult<EnrollmentDto> GetEnrollmentById(int id)
        {
            var result = _enrollment.GetEnrollmentById(id);
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<EnrollmentDto>(result));
        }

        // POST api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        {
            var enrollmentModel = _mapper.Map<Models.Enrollment>(enrollmentForCreateDto);
            _enrollment.CreateEnrollment(enrollmentModel);
            _enrollment.SaveChanges();

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollmentModel);

            //send sync
            try
            {
                await _paymentDataClient.CreateEnrollmentInPayment(enrollmentDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetEnrollmentById),
            new { Id = enrollmentDto.EnrollmentID }, enrollmentDto);
        }
        // public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        // {
        //     var enrollmentModel = _mapper.Map<Enrollment>(enrollmentForCreateDto);
        //     _enrollment.CreateEnrollment(enrollmentModel);
        //     _enrollment.SaveChanges();

        //     var enrollmentDto = _mapper.Map<EnrollmentForCreateDto>(enrollmentModel);

        //     //send sync
        //     try{
        //         await _paymentDataClient.CreateEnrollmentInPayment(enrollmentDto);
        //     }catch(Exception ex){
        //         Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        //     }

        //     return CreatedAtRoute(nameof(GetEnrollmentById),
        //     //new {Id=enrollmentDto.EnrollmentID}, enrollmentDto);
        //     new { Id = enrollmentDto.EnrollmentID }, enrollmentDto);
        // }

        // PUT api/<EnrollmentsController>/5

        // [Authorize(Roles = "admin,student")]
        // [HttpPut("{id}")]
        // public async Task<ActionResult<Enrollment>> Put(int id, [FromBody] EnrollmentForCreateDto enrollmentforCreateDto)
        // {
        //     try
        //     {
        //         var enrollment = _mapper.Map<Models.Enrollment>(enrollmentforCreateDto);
        //         var result = await _enrollment.Update(id.ToString(),enrollment);
        //         var enrollmentdto = _mapper.Map<Dtos.EnrollmentDto>(result);
        //         return Ok(enrollmentdto);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // // DELETE api/<EnrollmentsController>/5

        // [Authorize(Roles = "admin,student")]
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     try
        //     {
        //         await _enrollment.Delete(id.ToString());
        //         return Ok($"delete data id {id} berhasil");
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
       
        
    }
}
