using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MCQExamApi.Data;
using MCQExamApi.Models;
using AutoMapper;
using MCQExamApi.Dtos.Option;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IRootRepository<Option> _rootRepo;
        private readonly IMapper _mapper;

        public OptionsController(IRootRepository<Option> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Options
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OptionDTO>>> GetOptions()
        {
            try
            {
                var options = await _rootRepo.GetAllAsync();
                var optionDtos = _mapper.Map<ICollection<OptionDTO>>(options);
                return Ok(optionDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching options.");
            }
        }

        // GET: api/Options/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OptionDTO>> GetOption(int id)
        {
            try
            {
                var option = await _rootRepo.GetByIdAsync(id);
                if (option == null)
                {
                    return NotFound($"Option with ID {id} not found.");
                }

                var optionDto = _mapper.Map<OptionDTO>(option);
                return Ok(optionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the option.");
            }
        }

        // PUT: api/Options/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOption(int id, OptionDTO optionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != optionDto.OptionId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingOption = await _rootRepo.GetByIdAsync(id);
                if (existingOption == null)
                {
                    return NotFound($"Option with ID {id} not found.");
                }

                var option = _mapper.Map(optionDto, existingOption); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, option);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the option.");
            }
        }

        // POST: api/Options
        [HttpPost]
        public async Task<ActionResult<OptionDTO>> PostOption(OptionDTO optionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var option = _mapper.Map<Option>(optionDto);
                var createdOption = await _rootRepo.AddAsync(option);
                var createdOptionDto = _mapper.Map<OptionDTO>(createdOption);

                return CreatedAtAction(nameof(GetOption), new { id = createdOptionDto.OptionId }, createdOptionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the option.");
            }
        }

        // DELETE: api/Options/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            try
            {
                var option = await _rootRepo.GetByIdAsync(id);
                if (option == null)
                {
                    return NotFound($"Option with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the option.");
            }
        }
    }
}
