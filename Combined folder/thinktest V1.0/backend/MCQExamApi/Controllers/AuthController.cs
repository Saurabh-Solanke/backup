using MCQExamApi.Data;
using MCQExamApi.Dtos.Auth;
using MCQExamApi.interfaces;
using MCQExamApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCQExamApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly UserManager<ExamUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ExamUser> _signinManager;
        private readonly ExamContext _context;


        public AuthController(
            UserManager<ExamUser> userManager,
            ITokenService tokenService,
            SignInManager<ExamUser> signInManager,
            ExamContext context)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> ResiterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var examUser = new ExamUser
                {
                    PhoneNumber = registerDto.MobileNo,
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(examUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(examUser, registerDto.Role);
                    if (roleResult.Succeeded)
                    {
                        User newUser = new User
                        {
                            FirstName = registerDto.Firstname,
                            Email = registerDto.Email,
                            Password = registerDto.Password,
                            MobileNo = registerDto.MobileNo,
                            Role = registerDto.Role,
                            LastName = registerDto.Lastname,
                            ExamUserId = examUser.Id
                        };
                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();
                        return Ok(
                            new NewUserDto
                            {
                                UserId = newUser.UserId,
                                FirstName = examUser.UserName,
                                LastName = newUser.LastName,
                                Email = examUser.Email,
                                MobileNo = newUser.MobileNo,

                            }
                            );

                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid Email!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }
            else
            {
                var loggedUser = await _context.Users.FirstOrDefaultAsync(s => s.ExamUserId == user.Id);

                if (loggedUser == null)
                {
                    //Admin
                    var loggedInUser = new LoggedInUser
                    {
                        FirstName = user.UserName,
                        Email = user.Email,
                        Role = "Admin",
                        Token = await _tokenService.CreateToken(user),
                        Expiration = DateTime.Now.AddDays(3),
                    };
                    return Ok(loggedInUser);
                }
                else
                {
                    var loggedInUser = new LoggedInUser
                    {
                        FirstName = loggedUser.FirstName,
                        ExamUserId = user.Id.ToString(),
                        Email = user.Email,
                        LastName = loggedUser.LastName,
                        MobileNo = loggedUser.MobileNo,
                        Role = loggedUser.Role,
                        UserId = loggedUser.UserId,
                        Token = await _tokenService.CreateToken(user),
                        Expiration = DateTime.Now.AddDays(3),
                    };
                    return Ok(loggedInUser);
                }


            }

            return Unauthorized();
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Ok(false); // Email does not exist
            }
            return Ok(true); // Email exists
        }
    }
}
