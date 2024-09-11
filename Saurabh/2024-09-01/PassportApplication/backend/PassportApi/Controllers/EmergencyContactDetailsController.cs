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
    public class EmergencyContactDetailsController : ControllerBase
    {
        private readonly IRootRepository<EmergencyContactDetails> _rootRepo;
        private readonly IMapper _mapper;

        public EmergencyContactDetailsController(IRootRepository<EmergencyContactDetails> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/EmergencyContactDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmergencyContactDetailsDTO>>> GetEmergencyContactDetails()
        {
            try
            {
                var emergencyContactDetails = await _rootRepo.GetAllAsync();
                var emergencyContactDetailsDtos = _mapper.Map<IEnumerable<EmergencyContactDetailsDTO>>(emergencyContactDetails);
                return Ok(emergencyContactDetailsDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the emergency contact details.");
            }
        }

        // GET: api/EmergencyContactDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmergencyContactDetailsDTO>> GetEmergencyContactDetails(int id)
        {
            try
            {
                var emergencyContactDetails = await _rootRepo.GetByIdAsync(id);
                if (emergencyContactDetails == null)
                {
                    return NotFound($"Emergency contact details with ID {id} not found.");
                }

                var emergencyContactDetailsDto = _mapper.Map<EmergencyContactDetailsDTO>(emergencyContactDetails);
                return Ok(emergencyContactDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the emergency contact details.");
            }
        }

        // PUT: api/EmergencyContactDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergencyContactDetails(int id, EmergencyContactDetailsDTO emergencyContactDetailsDto)
        {
            if (id != emergencyContactDetailsDto.EmergencyContactDetailsId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var emergencyContactDetails = _mapper.Map<EmergencyContactDetails>(emergencyContactDetailsDto);
                await _rootRepo.UpdateAsync(id, emergencyContactDetails);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Emergency contact details with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the emergency contact details.");
            }
        }

        // POST: api/EmergencyContactDetails
        [HttpPost]
        public async Task<ActionResult<EmergencyContactDetailsDTO>> PostEmergencyContactDetails(EmergencyContactDetailsDTO emergencyContactDetailsDto)
        {
            try
            {
                var emergencyContactDetails = _mapper.Map<EmergencyContactDetails>(emergencyContactDetailsDto);
                var newEmergencyContactDetails = await _rootRepo.AddAsync(emergencyContactDetails);
                var newEmergencyContactDetailsDto = _mapper.Map<EmergencyContactDetailsDTO>(newEmergencyContactDetails);

                return CreatedAtAction(nameof(GetEmergencyContactDetails), new { id = newEmergencyContactDetailsDto.EmergencyContactDetailsId }, newEmergencyContactDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the emergency contact details.");
            }
        }

        // DELETE: api/EmergencyContactDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmergencyContactDetails(int id)
        {
            try
            {
                var emergencyContactDetails = await _rootRepo.GetByIdAsync(id);
                if (emergencyContactDetails == null)
                {
                    return NotFound($"Emergency contact details with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the emergency contact details.");
            }
        }
    }
}
