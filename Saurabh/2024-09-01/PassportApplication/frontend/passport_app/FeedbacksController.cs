using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportAPI.Data;
using PassportAPI.DTO;
using PassportAPI.DTO.Auth;
using PassportAPI.Interface;
using PassportAPI.Models;

namespace PassportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly PassportAPIContext _context;   
        private readonly IMapper _mapper;
        private readonly IRootRepository<Feedback> _feedbackRepo;

        public FeedbacksController(PassportAPIContext context, IMapper mapper, IRootRepository<Feedback> feedbackRepo)
        {
            _context = context;
            _feedbackRepo = feedbackRepo;
            _mapper = mapper;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<ICollection<FeedbackDTO>>> GetFeedback()
        {
            var  feedbacks = await _feedbackRepo.GetAllAsync();
            var feedbackDtos = _mapper.Map<ICollection<FeedbackDTO>>(feedbacks).ToList();
            return Ok(feedbackDtos);
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDTO>> GetFeedback(int id)
        {
            var feedback = await _feedbackRepo.GetByIdAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            var feedbackDto = _mapper.Map<FeedbackDTO>(feedback);

            return feedbackDto;
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, FeedbackDTO feedbackDTO)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDTO);
            await _feedbackRepo.UpdateAsync(feedback);

            return NoContent();
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedbackDTO>> PostFeedback(FeedbackDTO feedbackDTO)
        {
            var user = _context.User.Where(u => u.Email ==feedbackDTO.Email).First();
            feedbackDTO.CreatedOn = DateTime.Now;
            feedbackDTO.UpdatedOn = DateTime.Now;
            feedbackDTO.UserID = user.UserID;
            var feedback = _mapper.Map<Feedback>(feedbackDTO);
            var newFeedback = await _feedbackRepo.AddAsync(feedback);
            var newFeedbackDto = _mapper.Map<FeedbackDTO>(newFeedback);
            return Ok(newFeedbackDto);
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteFeedback(int id)
        {
            return await _feedbackRepo.DeleteAsync(id);
        }

        
    }
}
