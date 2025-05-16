using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;

namespace skillJas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentationController : ControllerBase
{
    private readonly IDocumentationService _documentationService;

    public DocumentationController(IDocumentationService documentationService)
    {
        _documentationService = documentationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentationDto dto,
        [FromHeader(Name = "X-Role")]
    string role)
    {
        if (string.IsNullOrEmpty(role) || role.ToLower() != "admin")
        {
            return Forbid("Access denied: only admins can create documentations.");
        }
        var id = await _documentationService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id }, dto);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var docs = await _documentationService.GetAllAsync();
        return Ok(docs);
    }
}
