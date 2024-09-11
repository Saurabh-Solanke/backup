using Lab243.BusinessLogic.Interfaces;
using Lab243.DataAccess.Entities;
using Lab243.DataAccess.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab243.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Course = studentDto.Course
            };
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            var existingStudent = _studentService.GetStudentById(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = studentDto.Name;
            existingStudent.Course = studentDto.Course;

            _studentService.UpdateStudent(existingStudent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
