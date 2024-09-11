using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Dtos.ApplicationForm;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantDetailsController : ControllerBase
    {
        private readonly IRootRepository<ApplicantDetails> _rootRepo;
        private readonly IMapper _mapper;

        public ApplicantDetailsController(IRootRepository<ApplicantDetails> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/ApplicantDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicantDetailsDTO>>> GetApplicantDetails()
        {
            try
            {
                var applicantDetails = await _rootRepo.GetAllAsync();
                var applicantDetailsDtos = _mapper.Map<IEnumerable<ApplicantDetailsDTO>>(applicantDetails);
                return Ok(applicantDetailsDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the applicant details.");
            }
        }

        // GET: api/ApplicantDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantDetailsDTO>> GetApplicantDetails(int id)
        {
            try
            {
                var applicantDetails = await _rootRepo.GetByIdAsync(id);
                if (applicantDetails == null)
                {
                    return NotFound($"Applicant details with ID {id} not found.");
                }

                var applicantDetailsDto = _mapper.Map<ApplicantDetailsDTO>(applicantDetails);
                return Ok(applicantDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the applicant details.");
            }
        }

        // PUT: api/ApplicantDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicantDetails(int id, ApplicantDetailsDTO applicantDetailsDto)
        {
            if (id != applicantDetailsDto.ApplicantDetailsTableID)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var applicantDetails = _mapper.Map<ApplicantDetails>(applicantDetailsDto);
                await _rootRepo.UpdateAsync(id, applicantDetails);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Applicant details with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the applicant details.");
            }
        }

        // POST: api/ApplicantDetails
        [HttpPost]
        public async Task<ActionResult<ApplicantDetailsDTO>> PostApplicantDetails(ApplicantDetailsDTO applicantDetailsDto)
        {
            try
            {
                var applicantDetails = _mapper.Map<ApplicantDetails>(applicantDetailsDto);
                var newApplicantDetails = await _rootRepo.AddAsync(applicantDetails);
                var newApplicantDetailsDto = _mapper.Map<ApplicantDetailsDTO>(newApplicantDetails);

                return CreatedAtAction(nameof(GetApplicantDetails), new { id = newApplicantDetailsDto.ApplicantDetailsTableID }, newApplicantDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the applicant details.");
            }
        }

        // DELETE: api/ApplicantDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicantDetails(int id)
        {
            try
            {
                var applicantDetails = await _rootRepo.GetByIdAsync(id);
                if (applicantDetails == null)
                {
                    return NotFound($"Applicant details with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the applicant details.");
            }
        }
    }
}
