using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge__Test_Backend.Data;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;
using Online_Knowledge__Test_Backend.RepositoryLayer;
using Online_Knowledge__Test_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Online_Knowledge__Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExamDbContext _examDbContext;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly UserManager<ExamUser> _userManager;
        private readonly SignInManager<ExamUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ExamUser> userManager,
                              SignInManager<ExamUser> signInManager,
                              IUnitOfWork unitOfWork, IMapper mapper,
                               ExamDbContext examDbContext,
                               IConfiguration config,
                               IJWTTokenService jWTTokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _examDbContext = examDbContext;
            _jwtTokenService = jWTTokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        //API for Login purpose 

        [HttpPost("login")]
        public async Task<ActionResult<LoggedInUser>> Login(LoginDto model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == model.UserEmail);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Email or Password is incorrect.");
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userData = _unitOfWork.UserRepository.FindByEmail(model.UserEmail);
                var loggedInUser = new LoggedInUser
                {
                    Email = userData.Result.UserEmail,
                    UserFullname = userData.Result.UserFullname,
                    UserId = userData.Result.UserId,
                    Token = await _jwtTokenService.GenerateToken(user),
                    Role = roles.FirstOrDefault(),
                    Expiration = DateTime.Now.AddHours(3),
                };
                return Ok(loggedInUser);
            }
        }


        // method for signup / register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            // Check if the username already exists
            var existingUser = await _unitOfWork.UserRepository.FindByEmail(model.UserEmail);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already exists. Please choose a different Email." });
            }
            var identityUser = new ExamUser
            {
                UserName = model.UserEmail,
                Email = model.UserEmail,
                PhoneNumber = model.UserMobileNo,

            };
            var result = await _userManager.CreateAsync(identityUser, model.UserPassword);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(identityUser, "User");

                // saving the details in user table
                var user = new User
                {
                    UserFullname = model.UserFullname,
                    UserPassword = model.UserPassword,
                    UserMobileNo = model.UserMobileNo,
                    UserEmail = model.UserEmail,
                    ExamUserId = identityUser.Id,
                    UserAddress = model.UserAddress,
                    UserPincode = model.UserPincode,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    IsActive = true
                };

                await _unitOfWork.UserRepository.AddAsync(user);

                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

    }
}
