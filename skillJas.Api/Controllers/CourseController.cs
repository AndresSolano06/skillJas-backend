using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;

namespace skillJas.Api.Controllers;

[ApiController]
[Route("api/Courses")]
[Authorize] 
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
    {
        var result = await _courseService.CreateCourseAsync(dto);
        return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var result = await _courseService.GetCourseByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }
}
