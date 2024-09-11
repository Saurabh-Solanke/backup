using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;
using PassportApi.Data;
using PassportApi.Dtos.Auth;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly UserManager<PassportUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<PassportUser> _signinManager;
        private readonly PassportDbContext _context;
        private readonly IEmailSender _emailSender;

        public AuthController(
            UserManager<PassportUser> userManager,
            ITokenService tokenService,
            SignInManager<PassportUser> signInManager,
            PassportDbContext context,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> ResiterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var passUser = new PassportUser
                {
                    PhoneNumber = registerDto.MobileNo,
                    UserName=registerDto.Email,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(passUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(passUser, "User");
                    if (roleResult.Succeeded)
                    {
                        User newUser = new User
                        {
                            Firstname = registerDto.Firstname,
                            Email = registerDto.Email,
                            Password = registerDto.Password,
                            MobileNo = registerDto.MobileNo,
                            Role =registerDto.Role,
                            Lastname = registerDto.Lastname,
                            UpdatedOn = new DateTime(),
                            PassportUserId=passUser.Id
                        };
                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();
                        return Ok(
                            new NewUserDto
                            {
                                UserId=newUser.UserId,
                                Firstname = passUser.UserName,
                                Lastname=newUser.Lastname,
                                Email = passUser.Email,
                                MobileNo=newUser.MobileNo,
                                
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
            }catch (Exception e)
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
                var loggedUser = await _context.Users.FirstOrDefaultAsync(s => s.PassportUserId == user.Id);
                if (loggedUser == null)
                {
                    //Admin
                    var loggedInUser = new LoggedInUser
                    {
                        Firstname = user.UserName,
                        Email = user.Email,
                        Role= "Admin",
                        Token = await _tokenService.CreateToken(user),
                        Expiration = DateTime.Now.AddDays(3),
                    };
                    return Ok(loggedInUser);
                }
                else
                {
                    var loggedInUser = new LoggedInUser
                    {
                        Firstname = loggedUser.Firstname,
                        PassportUserId=user.Id,
                        Email = user.Email,
                        Lastname = loggedUser.Lastname,
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
                if (user == null)
                {
                    return BadRequest("Invalid Request");
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Auth", new { token = resetToken, email = forgotPasswordDTO.Email }, Request.Scheme);

                // Compose the email message with a custom message and the reset link
                var emailMessage = $@"
                            <h1>Password Reset Request</h1>
                            <p>Hello {user.UserName},</p>
                            <p>You requested to reset your password. Please click the link below to reset your password:</p>
                            <p><a href='{resetLink}'>Reset Password</a></p>
                            <p>If you did not request a password reset, please ignore this email.</p>
                            <p>Thank you!</p>";

                // Send the password reset email
                await _emailSender.SendEmailAsync(forgotPasswordDTO.Email, "Password Reset", emailMessage);

                return Ok("Password reset link has been sent to your email.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
