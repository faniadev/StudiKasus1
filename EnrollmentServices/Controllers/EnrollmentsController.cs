using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrollmentServices.Data;
using EnrollmentServices.Dtos;
using EnrollmentServices.Models;

namespace EnrollmentServices.Controllers
{
    [Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollment _enrollment;
        private IMapper _mapper;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper )
        {
            _enrollment = enrollment;
            _mapper = mapper;
        }

        // GET: api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> Get()
        //public async Task<ActionResult<IEnumerable<EnrollmentDto>>> Get()
        {
            var results = await _enrollment.GetAll();
            return results;
            //return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(results));
            
            
        }

        // GET api/<EnrollmentsController>/5

        [Authorize(Roles = "admin,student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> Get(int id)
        {
            var result = await _enrollment.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<EnrollmentDto>(result));
        }

        // POST api/<EnrollmentsController>

        [Authorize(Roles = "admin,student")]
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> Post([FromBody] EnrollmentForCreateDto enrollmentforCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Models.Enrollment>(enrollmentforCreateDto);
                var result = await _enrollment.Insert(enrollment);
                var enrollmentdto = _mapper.Map<Dtos.EnrollmentDto>(result);
                return Ok(enrollmentdto);
       
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EnrollmentsController>/5

        [Authorize(Roles = "admin,student")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Enrollment>> Put(int id, [FromBody] EnrollmentForCreateDto enrollmentforCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Models.Enrollment>(enrollmentforCreateDto);
                var result = await _enrollment.Update(id.ToString(),enrollment);
                var enrollmentdto = _mapper.Map<Dtos.EnrollmentDto>(result);
                return Ok(enrollmentdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EnrollmentsController>/5

        [Authorize(Roles = "admin,student")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.Delete(id.ToString());
                return Ok($"delete data id {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
