using Microsoft.AspNetCore.Mvc;
using SchoolApi.Domain.Entities;
using SchoolApi.Application.Interfaces;
using SchoolApi.Application.DTOs;

namespace SchoolApi.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("/api/students")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return student != null ? Ok(student) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var student = new Student
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate
            };

            var created = await _studentService.CreateAsync(student);
            return StatusCode(200, new {
                status = 200,
                message = "Student created successfully",
                data = created
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest(new{
                    message = "Something wrong in your request"
                });

            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound(new {
                    message = "Student Not Found"
                });

            existingStudent.Name = student.Name;
            existingStudent.BirthDate = student.BirthDate;

            await _studentService.UpdateAsync(existingStudent);
            return Ok(new{
                message = "Success. Data updated !",
                data = existingStudent
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
