using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Dtos.ApplicationForm;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackComplaintsController : ControllerBase
    {
        private readonly IRootRepository<FeedbackComplaint> _rootRepo;
        private readonly IMapper _mapper;

        public FeedbackComplaintsController(IRootRepository<FeedbackComplaint> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/FeedbackComplaints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackComplaintDTO>>> GetFeedbackComplaints()
        {
            try
            {
                var feedbackComplaints = await _rootRepo.GetAllAsync();
                var feedbackComplaintDtos = _mapper.Map<IEnumerable<FeedbackComplaintDTO>>(feedbackComplaints);
                return Ok(feedbackComplaintDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching feedback complaints.");
            }
        }

        // GET: api/FeedbackComplaints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackComplaintDTO>> GetFeedbackComplaint(int id)
        {
            try
            {
                var feedbackComplaint = await _rootRepo.GetByIdAsync(id);
                if (feedbackComplaint == null)
                {
                    return NotFound($"Feedback complaint with ID {id} not found.");
                }

                var feedbackComplaintDto = _mapper.Map<FeedbackComplaintDTO>(feedbackComplaint);
                return Ok(feedbackComplaintDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the feedback complaint.");
            }
        }

        // PUT: api/FeedbackComplaints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackComplaint(int id, FeedbackComplaintDTO feedbackComplaintDto)
        {
            if (id != feedbackComplaintDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var feedbackComplaint = _mapper.Map<FeedbackComplaint>(feedbackComplaintDto);
                await _rootRepo.UpdateAsync(id, feedbackComplaint);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Feedback complaint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the feedback complaint.");
            }
        }

        // POST: api/FeedbackComplaints
        [HttpPost]
        public async Task<ActionResult<FeedbackComplaintDTO>> PostFeedbackComplaint(FeedbackComplaintDTO feedbackComplaintDto)
        {
            try
            {
                var feedbackComplaint = _mapper.Map<FeedbackComplaint>(feedbackComplaintDto);
                var newFeedbackComplaint = await _rootRepo.AddAsync(feedbackComplaint);
                var newFeedbackComplaintDto = _mapper.Map<FeedbackComplaintDTO>(newFeedbackComplaint);

                return CreatedAtAction(nameof(GetFeedbackComplaint), new { id = newFeedbackComplaintDto.Id }, newFeedbackComplaintDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the feedback complaint.");
            }
        }

        // DELETE: api/FeedbackComplaints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackComplaint(int id)
        {
            try
            {
                var feedbackComplaint = await _rootRepo.GetByIdAsync(id);
                if (feedbackComplaint == null)
                {
                    return NotFound($"Feedback complaint with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the feedback complaint.");
            }
        }
    }
}
