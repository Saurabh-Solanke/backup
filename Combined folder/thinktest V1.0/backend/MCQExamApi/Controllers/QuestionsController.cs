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
using MCQExamApi.Dtos.Question;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IRootRepository<Question> _rootRepo;
        private readonly IMapper _mapper;

        public QuestionsController(IRootRepository<Question> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {
            try
            {
                var questions = await _rootRepo.GetAllAsync();
                var questionDtos = _mapper.Map<ICollection<QuestionDTO>>(questions);
                return Ok(questionDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching questions.");
            }
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestion(int id)
        {
            try
            {
                var question = await _rootRepo.GetByIdAsync(id);
                if (question == null)
                {
                    return NotFound($"Question with ID {id} not found.");
                }

                var questionDto = _mapper.Map<QuestionDTO>(question);
                return Ok(questionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the question.");
            }
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionDTO questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionDto.QuestionId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingQuestion = await _rootRepo.GetByIdAsync(id);
                if (existingQuestion == null)
                {
                    return NotFound($"Question with ID {id} not found.");
                }

                var question = _mapper.Map(questionDto, existingQuestion); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, question);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the question.");
            }
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> PostQuestion(int examId, [FromBody] QuestionDTO questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = _mapper.Map<Question>(questionDto);
                question.ExamId=examId;
                var createdQuestion = await _rootRepo.AddAsync(question);
                var createdQuestionDto = _mapper.Map<QuestionDTO>(createdQuestion);

                return CreatedAtAction(nameof(GetQuestion), new { id = createdQuestionDto.QuestionId }, createdQuestionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the question.");
            }
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await _rootRepo.GetByIdAsync(id);
                if (question == null)
                {
                    return NotFound($"Question with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the question.");
            }
        }
    }
}
