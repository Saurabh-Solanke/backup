using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        private readonly IMapper _mapper;

        public SectionController(ISectionService sectionService, IMapper mapper)
        {
            _sectionService = sectionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSections()
        {
            var sections = await _sectionService.GetAllSectionsAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(int id)
        {
            var section = await _sectionService.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound();

            return Ok(section);
        }


        [HttpGet("Exam/{examId}")]
        public async Task<IActionResult> GetSectionsByExamId(int examId)
        {
            var sections = await _sectionService.GetSectionsByExamIdAsync(examId);
            if (sections == null || !sections.Any()) // Use .Any() instead of .Count
                return NotFound("No sections found for the specified exam.");

            return Ok(sections);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _sectionService.CreateSectionAsync(sectionDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(int id, [FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _sectionService.UpdateSectionAsync(id, sectionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            await _sectionService.DeleteSectionAsync(id);
            return NoContent();
        }

        [HttpGet("TotalMarks/{sectionId}")]
        public async Task<IActionResult> GetTotalMarksForSection(int sectionId)
        {
            var totalMarks = await _sectionService.CalculateTotalMarksAsync(sectionId);
            if (totalMarks == 0)
                return NotFound("Section not found or no correct answers available.");

            return Ok(new { SectionId = sectionId, TotalMarks = totalMarks });
        }
    }
}
