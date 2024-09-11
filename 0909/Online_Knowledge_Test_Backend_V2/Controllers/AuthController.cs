using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer;
using Online_Knowledge_Test_Backend_V2.Services.Implementation;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //inject auth service interface here
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //API for Login purpose 

        [HttpPost("Login")]
        public async Task<ActionResult<LoggedInUser>> Login(LoginDto model)
        {
            //call IAuthService method
            var result = await _authService.Login(model);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid Credentials" });
            }
            return Ok(result);
        }


        // method for signup / register
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            // Call the AuthService Register method
            var result = await _authService.Register(model);

            if (!result.Succeeded)
            {
                // Return the errors if registration failed
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User registered successfully" });
        }
    }
}
