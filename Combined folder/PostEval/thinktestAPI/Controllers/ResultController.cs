using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.RepositoryLayer;

namespace Online_Knowledge__Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ExamDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResultController(ExamDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // get the result of user
        [HttpGet("getResultbyUserId/{id}")]
        public async Task<IActionResult> GetResultOfUser(int id)
        {
            try
            {
                IEnumerable<UserResultDto> result = await _unitOfWork.ResultRepository.GetUserResult(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
