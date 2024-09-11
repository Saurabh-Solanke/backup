using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("TestsPerDay")]
        public async Task<IActionResult> GetTestsPerDay()
        {
            var results = await _reportService.GetTestsPerDay();
            var report = results
                .GroupBy(r => r.CompletedDate.Date)
                .Select(g => new { Date = g.Key, NumberOfTests = g.Count() })
                .ToList();

            return Ok(report);
        }

        [HttpGet("FinishedBeforeTime")]
        public async Task<IActionResult> GetTestsFinishedBeforeTime()
        {
            var results = await _reportService.GetTestsFinishedBeforeTime();
            return Ok(results);
        }

        [HttpGet("AutoSubmittedAfter30Mins")]
        public async Task<IActionResult> GetTestsAutoSubmittedAfter30Mins()
        {
            var results = await _reportService.GetAutoSubmittedTests();
            return Ok(results);
        }

        [HttpGet("MarkedForReview")]
        public async Task<IActionResult> GetTestsMarkedForReview()
        {
            var results = await _reportService.GetMarkedForReviewTests();
            return Ok(results);
        }

        [HttpGet("QuestionsWithImages")]
        public async Task<IActionResult> GetQuestionsWithImages()
        {
            var results = await _reportService.GetQuestionsWithImages();
            return Ok(results);
        }

        [HttpGet("QuestionsWithVideos")]
        public async Task<IActionResult> GetQuestionsWithVideos()
        {
            var results = await _reportService.GetQuestionsWithVideos();
            return Ok(results);
        }

        [HttpGet("CountImageQuestions")]
        public async Task<IActionResult> GetImageQuestionsCount()
        {
            var count = await _reportService.GetImageQuestionsCount();
            return Ok(new { ImageQuestionsCount = count });
        }

        [HttpGet("CountVideoQuestions")]
        public async Task<IActionResult> GetVideoQuestionsCount()
        {
            var count = await _reportService.GetVideoQuestionsCount();
            return Ok(new { VideoQuestionsCount = count });
        }

        [HttpGet("Top10Students/{examId}")]
        public async Task<IActionResult> GetTop10StudentsByPercentile(int examId)
        {
            var top10Students = await _reportService.GetTop10StudentsByPercentile(examId);
            if (top10Students == null)
            {
                return NotFound($"No exam results found for exam ID: {examId}.");
            }

            return Ok(top10Students);
        }

        [HttpGet("TestsPerDayInRange")]
        public async Task<IActionResult> GetTestsPerDayInRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var results = await _reportService.GetTestsPerDayInRange(startDate, endDate);
            var report = results
                .GroupBy(r => r.CompletedDate.Date)
                .Select(g => new { Date = g.Key, NumberOfTests = g.Count() })
                .ToList();

            return Ok(report);
        }

        [HttpGet("FinishedBeforeTimeInRange")]
        public async Task<IActionResult> GetTestsFinishedBeforeTimeInRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var results = await _reportService.GetTestsFinishedBeforeTimeInRange(startDate, endDate);
            if (!results.Any())
            {
                return NotFound("No tests finished before 80% of the time in the given date range.");
            }

            return Ok(results);
        }

        [HttpGet("AutoSubmittedAfter30MinsInRange")]
        public async Task<IActionResult> GetAutoSubmittedTestsInRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var results = await _reportService.GetAutoSubmittedTestsInRange(startDate, endDate);
            if (!results.Any())
            {
                return NotFound("No auto-submitted tests found within the given date range.");
            }

            return Ok(results);
        }

        [HttpGet("MarkedForReviewInRange")]
        public async Task<IActionResult> GetTestsMarkedForReviewInRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var results = await _reportService.GetMarkedForReviewTestsInRange(startDate, endDate);
            if (!results.Any())
            {
                return NotFound("No tests found where at least 50% of questions were marked for review within the given date range.");
            }

            return Ok(results);
        }
    }
}
