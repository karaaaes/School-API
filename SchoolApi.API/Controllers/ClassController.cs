using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.Interfaces;
using SchoolApi.Application.DTOs;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Controllers;

[ApiController]
[Route("api/class")]
public class ClassController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpGet]
    [Route("api/classes")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _classService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _classService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClassCreateDto dto)
        {
            if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var classEntity = new Class
        {
            Name = dto.Name
        };

        var created = await _classService.CreateAsync(classEntity);
        return StatusCode(200, new {
            status = 200,
            message = "Class created successfully",
            data = created
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Class classEntity)
    {
        if (id != classEntity.Id)
            return BadRequest();

        await _classService.UpdateAsync(classEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _classService.DeleteAsync(id);
        return NoContent();
    }
}
