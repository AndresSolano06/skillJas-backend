﻿using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetActiveCourses([FromQuery] PaginationQuery query)
    {
        var result = await _courseService.GetCoursesAsync(query.Page, query.PageSize, query.Category);
        return Ok(result);
    }

    [HttpPatch("{id}/disable")]
    public async Task<IActionResult> DisableCourse(
    int id,
    [FromHeader(Name = "X-Role")] string role)
    {
        if (string.IsNullOrEmpty(role) || role.ToLower() != "admin")
        {
            return Forbid("Access denied: only admins can disable courses.");
        }

        var result = await _courseService.DisableAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/enable")]
    public async Task<IActionResult> EnableCourse(
    int id,
    [FromHeader(Name = "X-Role")] string role)
    {
        if (string.IsNullOrEmpty(role) || role.ToLower() != "admin")
        {
            return Forbid("Access denied: only admins can enable courses.");
        }

        var result = await _courseService.EnableAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNormalizedCategories()
    {
        var result = await _courseService.GetNormalizedCategoriesAsync();
        return Ok(result);
    }


}
