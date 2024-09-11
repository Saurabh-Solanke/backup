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
using MCQExamApi.Dtos.StudentAnswer;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnswersController : ControllerBase
    {
        private readonly IRootRepository<StudentAnswer> _rootRepo;
        private readonly IMapper _mapper;

        public StudentAnswersController(IRootRepository<StudentAnswer> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/StudentAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentAnswerDTO>>> GetStudentAnswers()
        {
            try
            {
                var studentAnswers = await _rootRepo.GetAllAsync();
                var studentAnswerDtos = _mapper.Map<ICollection<StudentAnswerDTO>>(studentAnswers);
                return Ok(studentAnswerDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching student answers.");
            }
        }

        // GET: api/StudentAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentAnswerDTO>> GetStudentAnswer(int id)
        {
            try
            {
                var studentAnswer = await _rootRepo.GetByIdAsync(id);
                if (studentAnswer == null)
                {
                    return NotFound($"Student answer with ID {id} not found.");
                }

                var studentAnswerDto = _mapper.Map<StudentAnswerDTO>(studentAnswer);
                return Ok(studentAnswerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the student answer.");
            }
        }

        // PUT: api/StudentAnswers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentAnswer(int id, StudentAnswerDTO studentAnswerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentAnswerDto.AnswerId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingStudentAnswer = await _rootRepo.GetByIdAsync(id);
                if (existingStudentAnswer == null)
                {
                    return NotFound($"Student answer with ID {id} not found.");
                }

                var studentAnswer = _mapper.Map(studentAnswerDto, existingStudentAnswer); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, studentAnswer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the student answer.");
            }
        }

        // POST: api/StudentAnswers
        [HttpPost]
        public async Task<ActionResult<StudentAnswerDTO>> PostStudentAnswer(StudentAnswerDTO studentAnswerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var studentAnswer = _mapper.Map<StudentAnswer>(studentAnswerDto);
                var createdStudentAnswer = await _rootRepo.AddAsync(studentAnswer);
                var createdStudentAnswerDto = _mapper.Map<StudentAnswerDTO>(createdStudentAnswer);

                return CreatedAtAction(nameof(GetStudentAnswer), new { id = createdStudentAnswerDto.AnswerId }, createdStudentAnswerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the student answer.");
            }
        }

        // DELETE: api/StudentAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAnswer(int id)
        {
            try
            {
                var studentAnswer = await _rootRepo.GetByIdAsync(id);
                if (studentAnswer == null)
                {
                    return NotFound($"Student answer with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the student answer.");
            }
        }
    }
}
