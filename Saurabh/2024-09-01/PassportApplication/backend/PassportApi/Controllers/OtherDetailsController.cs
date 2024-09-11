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
    public class OtherDetailsController : ControllerBase
    {
        private readonly IRootRepository<OtherDetails> _rootRepo;
        private readonly IMapper _mapper;

        public OtherDetailsController(IRootRepository<OtherDetails> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/OtherDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherDetailsDTO>>> GetOtherDetails()
        {
            try
            {
                var otherDetails = await _rootRepo.GetAllAsync();
                var otherDetailsDtos = _mapper.Map<IEnumerable<OtherDetailsDTO>>(otherDetails);
                return Ok(otherDetailsDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching other details.");
            }
        }

        // GET: api/OtherDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtherDetailsDTO>> GetOtherDetails(int id)
        {
            try
            {
                var otherDetails = await _rootRepo.GetByIdAsync(id);
                if (otherDetails == null)
                {
                    return NotFound($"Other details with ID {id} not found.");
                }

                var otherDetailsDto = _mapper.Map<OtherDetailsDTO>(otherDetails);
                return Ok(otherDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the other details.");
            }
        }

        // PUT: api/OtherDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherDetails(int id, OtherDetailsDTO otherDetailsDto)
        {
            if (id != otherDetailsDto.OtherDetailsId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var otherDetails = _mapper.Map<OtherDetails>(otherDetailsDto);
                await _rootRepo.UpdateAsync(id, otherDetails);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Other details with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the other details.");
            }
        }

        // POST: api/OtherDetails
        [HttpPost]
        public async Task<ActionResult<OtherDetailsDTO>> PostOtherDetails(OtherDetailsDTO otherDetailsDto)
        {
            try
            {
                var otherDetails = _mapper.Map<OtherDetails>(otherDetailsDto);
                var newOtherDetails = await _rootRepo.AddAsync(otherDetails);
                var newOtherDetailsDto = _mapper.Map<OtherDetailsDTO>(newOtherDetails);

                return CreatedAtAction(nameof(GetOtherDetails), new { id = newOtherDetailsDto.OtherDetailsId }, newOtherDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the other details.");
            }
        }

        // DELETE: api/OtherDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtherDetails(int id)
        {
            try
            {
                var otherDetails = await _rootRepo.GetByIdAsync(id);
                if (otherDetails == null)
                {
                    return NotFound($"Other details with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the other details.");
            }
        }
    }
}
