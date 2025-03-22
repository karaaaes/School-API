using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.Interfaces;
using SchoolApi.Application.DTOs;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Controllers;

[ApiController]
[Route("api/subject")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    [Route("api/subjects")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _subjectService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _subjectService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubjectCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var subject = new Subject
        {
            Title = dto.Title
        };

        var created = await _subjectService.CreateAsync(subject);
        return StatusCode(200, new {
            status = 200,
            message = "Subject created successfully",
            data = created
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Subject subject)
    {
        if (id != subject.Id)
            return BadRequest();

        await _subjectService.UpdateAsync(subject);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _subjectService.DeleteAsync(id);
        return NoContent();
    }
}
