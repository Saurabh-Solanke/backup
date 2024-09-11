using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, IOptionService optionService, IMapper mapper)
        {
            _questionService = questionService;
            _optionService = optionService;
            _mapper = mapper;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();

            var questionDto = _mapper.Map<QuestionDto>(question);
            return Ok(questionDto);
        }

        // GET: api/Question/Exam/5
        [HttpGet("Exam/{examId}")]
        public async Task<IActionResult> GetQuestionsByExamId(int examId)
        {
            var questions = await _questionService.GetQuestionsByExamIdAsync(examId);
            if (questions == null || !questions.Any())
                return NotFound("No questions found for the specified exam.");

            var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        // POST: api/Question
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the Question and Options together in one go
            var question = _mapper.Map<Question>(createQuestionDto);

            // Add the Question with its related Options in one transaction
            await _questionService.CreateQuestionAsync(question);

            var questionDto = _mapper.Map<QuestionDto>(question);
            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, questionDto);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();

            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}
