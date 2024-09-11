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
using MCQExamApi.Dtos.Exam;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IRootRepository<Exam> _rootRepo;
        private readonly IMapper _mapper;
        private readonly IExamRepository _examRepo;

        public ExamsController(IRootRepository<Exam> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDTO>>> GetExams()
        {
            try
            {
                var exams = await _rootRepo.GetAllAsync();
                var examDtos = _mapper.Map<ICollection<ExamDTO>>(exams);
                return Ok(examDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching exams.");
            }
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDTO>> GetExam(int id)
        {
            try
            {
                var exam = await _rootRepo.GetQueryable()
                                          .Include(e => e.Questions)
                                          .ThenInclude(q => q.Options)
                                          .FirstOrDefaultAsync(e => e.ExamId == id);
                if (exam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }

                // Map the Exam entity to the ExamDetailsDTO
                var examDetailsDto = _mapper.Map<ExamRespDTO>(exam);
                return Ok(examDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the exam.");
            }
        }

        // PUT: api/Exams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, ExamDTO examDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examDto.ExamId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingExam = await _rootRepo.GetByIdAsync(id);
                if (existingExam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }

                var exam = _mapper.Map(examDto, existingExam); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, exam);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the exam.");
            }
        }

        // POST: api/Exams
        [HttpPost]
        public async Task<ActionResult<ExamDTO>> PostExam(ExamDTO examDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var exam = _mapper.Map<Exam>(examDto);
                var createdExam = await _rootRepo.AddAsync(exam);
                var createdExamDto = _mapper.Map<ExamDTO>(createdExam);

                return CreatedAtAction(nameof(GetExam), new { id = createdExamDto.ExamId }, createdExamDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the exam.");
            }
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                var exam = await _rootRepo.GetByIdAsync(id);
                if (exam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the exam.");
            }
        }
    }
}
