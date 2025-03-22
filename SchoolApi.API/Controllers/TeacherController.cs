using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.Interfaces;
using SchoolApi.Application.DTOs;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Controllers;

[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    public TeacherController(ITeacherService teacherService) => _teacherService = teacherService;

    [HttpGet]
    [Route("/api/teachers")]
    public async Task<IActionResult> GetAll() => Ok(await _teacherService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        return teacher is null ? NotFound() : Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            var teacher = new Teacher
            {
                Name = dto.Name
            };

            var created = await _teacherService.CreateAsync(teacher);

            return StatusCode(200, new {
                status = 200,
                message = "Teacher created successfully",
                data = created
            });
        }
        catch (Exception ex)
        {    
            return StatusCode(500, new {
                status = 500,
                message = "Internal Server Error",
                error = ex.Message
            });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Teacher teacher)
    {
        if (id != teacher.Id) return BadRequest();
        await _teacherService.UpdateAsync(teacher);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);
        return NoContent();
    }
}
