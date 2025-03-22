using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.Interfaces;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Controllers;

[ApiController]
[Route("api/enrollment")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _enrollmentService.GetAllAsync();
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var enrollment = await _enrollmentService.GetByIdAsync(id);
        return enrollment is null ? NotFound() : Ok(enrollment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Enrollment enrollment)
    {
        var result = await _enrollmentService.CreateAsync(enrollment);
        return StatusCode(200, new{
            status = 200,
            message = "Enrollment created successfully",
            data = result
         });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Enrollment enrollment)
    {
        if (id != enrollment.Id) return BadRequest();
        await _enrollmentService.UpdateAsync(enrollment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _enrollmentService.DeleteAsync(id);
        return NoContent();
    }
}
