using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        private readonly IExamResultService _examResultService;

        public ExamResultController(IExamResultService examResultService)
        {
            _examResultService = examResultService;
        }

        [HttpPost("Submit")]
        public async Task<IActionResult> SubmitResult([FromBody] SubmitResultDto submitResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var examResultDto = await _examResultService.SubmitResultAsync(submitResultDto);
            return Ok(examResultDto);
        }

        [HttpGet("GetAllResults")]
        public async Task<IActionResult> GetAllResults()
        {
            var resultDtos = await _examResultService.GetAllResultsAsync();
            return Ok(resultDtos);
        }

        [HttpGet("GetResultsByUser/{userId}")]
        public async Task<IActionResult> GetResultsByUser(string userId)
        {
            var resultDtos = await _examResultService.GetResultsByUserAsync(userId);
            if (resultDtos == null || !resultDtos.Any())
                return NotFound("No results found for the given user.");

            return Ok(resultDtos);
        }
    }
}
