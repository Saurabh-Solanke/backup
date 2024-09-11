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
using MCQExamApi.Dtos.Result;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IRootRepository<Result> _rootRepo;
        private readonly IMapper _mapper;

        public ResultsController(IRootRepository<Result> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDTO>>> GetResults()
        {
            try
            {
                var results = await _rootRepo.GetAllAsync();
                var resultDtos = _mapper.Map<ICollection<ResultDTO>>(results);
                return Ok(resultDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching results.");
            }
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDTO>> GetResult(int id)
        {
            try
            {
                var result = await _rootRepo.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Result with ID {id} not found.");
                }

                var resultDto = _mapper.Map<ResultDTO>(result);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the result.");
            }
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, ResultDTO resultDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resultDto.ResultId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingResult = await _rootRepo.GetByIdAsync(id);
                if (existingResult == null)
                {
                    return NotFound($"Result with ID {id} not found.");
                }

                var result = _mapper.Map(resultDto, existingResult); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, result);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the result.");
            }
        }

        // POST: api/Results
        [HttpPost]
        public async Task<ActionResult<ResultDTO>> PostResult(ResultDTO resultDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _mapper.Map<Result>(resultDto);
                var createdResult = await _rootRepo.AddAsync(result);
                var createdResultDto = _mapper.Map<ResultDTO>(createdResult);

                return CreatedAtAction(nameof(GetResult), new { id = createdResultDto.ResultId }, createdResultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the result.");
            }
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            try
            {
                var result = await _rootRepo.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Result with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the result.");
            }
        }
    }
}
