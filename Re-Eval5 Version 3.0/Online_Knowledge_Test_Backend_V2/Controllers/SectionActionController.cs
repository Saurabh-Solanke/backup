using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionActionController : ControllerBase
    {
        private readonly ISectionActionService _sectionActionService;
        private readonly IMapper _mapper;

        public SectionActionController(ISectionActionService sectionActionService, IMapper mapper)
        {
            _sectionActionService = sectionActionService;
            _mapper = mapper;
        }

        [HttpGet("exam/{examId}")]
        public async Task<IActionResult> GetSectionsByExamId(int examId)
        {
            var sections = await _sectionActionService.GetSectionsByExamIdAsync(examId);
            return Ok(sections);
        }

        [HttpGet("{sectionId}")]
        public async Task<IActionResult> GetSectionById(int sectionId)
        {
            var section = await _sectionActionService.GetSectionByIdAsync(sectionId);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionService.CreateSectionAsync(sectionDto);
            return Ok();
        }

        [HttpPut("{sectionId}")]
        public async Task<IActionResult> UpdateSection(int sectionId, [FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionService.UpdateSectionAsync(sectionId, sectionDto);
            return NoContent();
        }

        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteSection(int sectionId)
        {
            await _sectionActionService.DeleteSectionAsync(sectionId);
            return NoContent();
        }

        [HttpPost("{sectionId}/questions")]
        public async Task<IActionResult> AddQuestion(int sectionId, [FromBody] QuestionPostDTO questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionService.AddQuestionAsync(sectionId, questionDto);
            return Ok();
        }

        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            await _sectionActionService.DeleteQuestionAsync(questionId);
            return NoContent();
        }
    }
}
