using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;
using System.Data;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExamDbContext _examDbContext;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ExamDbContext examDbContext, IJWTTokenService jwtTokenService, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _examDbContext = examDbContext;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<LoggedInUser> Login(LoginDto model)
        {
            // Get the user by email
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email.ToLower());

            if (user == null)
            {
                return null; // Invalid email
            }

            // Check if the password is correct
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return null; // Invalid password
            }

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            // Get user data from the repository
            var userData = await _unitOfWork.UserRepository.FindByEmail(model.Email);

            // Create the LoggedInUser object to return
            var loggedInUser = new LoggedInUser
            {
                Email = userData.Email,
                FullName = userData.Fullname,
                UserId = userData.Id,
                Token = await _jwtTokenService.GenerateToken(user),
                Role = roles.FirstOrDefault(),
                Expiration = DateTime.Now.AddHours(3) // Token expiration
            };

            return loggedInUser;
        }


        public async Task<IdentityResult> Register(RegisterDto model)
        {
            // Check if email already exists
            if( await _userManager.Users.AnyAsync(u => u.Email == model.Email.ToLower()))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already registered" });
            }

            // Create a new IdentityUser
            var identityUser = new User
            {
                Fullname = model.FullName,
                Email = model.Email.ToLower(),
                MobileNo = model.MobileNo,
                UserName = model.Email.ToLower(),
                EmailConfirmed = true
            };

            // Create the user in Identity
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {
                return result; // Return the errors
            }

            // Add the user to the "User" role
            await _userManager.AddToRoleAsync(identityUser, "User");

            // Create the user in the application database
            var user = new User
            {
                Fullname = model.FullName,
                MobileNo = model.MobileNo,
                Email = model.Email,
                Id = identityUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                IsActive = true
            };

            return IdentityResult.Success; // Indicate successful registration
        }
    }
}
