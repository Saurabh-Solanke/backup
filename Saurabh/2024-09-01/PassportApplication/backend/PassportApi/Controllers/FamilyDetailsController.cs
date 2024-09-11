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
    public class FamilyDetailsController : ControllerBase
    {
        private readonly IRootRepository<FamilyDetails> _rootRepo;
        private readonly IMapper _mapper;

        public FamilyDetailsController(IRootRepository<FamilyDetails> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/FamilyDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyDetailsDTO>>> GetFamilyDetails()
        {
            try
            {
                var familyDetails = await _rootRepo.GetAllAsync();
                var familyDetailsDtos = _mapper.Map<IEnumerable<FamilyDetailsDTO>>(familyDetails);
                return Ok(familyDetailsDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the family details.");
            }
        }

        // GET: api/FamilyDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyDetailsDTO>> GetFamilyDetails(int id)
        {
            try
            {
                var familyDetails = await _rootRepo.GetByIdAsync(id);
                if (familyDetails == null)
                {
                    return NotFound($"Family details with ID {id} not found.");
                }

                var familyDetailsDto = _mapper.Map<FamilyDetailsDTO>(familyDetails);
                return Ok(familyDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the family details.");
            }
        }

        // PUT: api/FamilyDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamilyDetails(int id, FamilyDetailsDTO familyDetailsDto)
        {
            if (id != familyDetailsDto.FamilyDetailsId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var familyDetails = _mapper.Map<FamilyDetails>(familyDetailsDto);
                await _rootRepo.UpdateAsync(id, familyDetails);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Family details with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the family details.");
            }
        }

        // POST: api/FamilyDetails
        [HttpPost]
        public async Task<ActionResult<FamilyDetailsDTO>> PostFamilyDetails(FamilyDetails familyDetails)
        {
            try
            {
                //var familyDetails = _mapper.Map<FamilyDetails>(familyDetailsDto);
                var newFamilyDetails = await _rootRepo.AddAsync(familyDetails);
                //var newFamilyDetailsDto = _mapper.Map<FamilyDetailsDTO>(newFamilyDetails);

                return CreatedAtAction(nameof(GetFamilyDetails), new { id = newFamilyDetails.FamilyDetailsId }, newFamilyDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the family details.");
            }
        }

        // DELETE: api/FamilyDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyDetails(int id)
        {
            try
            {
                var familyDetails = await _rootRepo.GetByIdAsync(id);
                if (familyDetails == null)
                {
                    return NotFound($"Family details with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the family details.");
            }
        }
    }
}
