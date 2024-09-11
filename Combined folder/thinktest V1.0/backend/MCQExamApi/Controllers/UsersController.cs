using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MCQExamApi.Data;
using MCQExamApi.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MCQExamApi.Dtos.User;
using MCQExamApi.interfaces;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRootRepository<User> _rootRepo;
        private readonly IMapper _mapper;

        public UsersController(IRootRepository<User> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        [Authorize(Roles = "Student")]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _rootRepo.GetAllAsync();
                var userDtos = _mapper.Map<ICollection<UserDTO>>(users);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }

        [Authorize(Roles = "Student")]
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                var user = await _rootRepo.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var userDto = _mapper.Map<UserDTO>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the user.");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDto.UserId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingUser = await _rootRepo.GetByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var user = _mapper.Map(userDto, existingUser); // Update the existing entity with the DTO data
                await _rootRepo.UpdateAsync(id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                var createdUser = await _rootRepo.AddAsync(user);
                var createdUserDto = _mapper.Map<UserDTO>(createdUser);

                return CreatedAtAction(nameof(GetUser), new { id = createdUserDto.UserId }, createdUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _rootRepo.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
