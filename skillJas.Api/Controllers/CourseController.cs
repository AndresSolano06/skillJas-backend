using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;

namespace skillJas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Verifica JWT de Clerk
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseDto dto,
        [FromHeader(Name = "X-Role")] string role)
    {
        if (string.IsNullOrEmpty(role) || role.ToLower() != "admin")
        {
            return Forbid("Access denied: only admins can create courses.");
        }

        var courseId = await _courseService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetCourseById), new { id = courseId }, courseId);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var result = await _courseService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

}
