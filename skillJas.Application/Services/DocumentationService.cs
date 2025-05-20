using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;
using skillJas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace skillJas.Application.Services;

public class DocumentationService : IDocumentationService
{
    private readonly ISkillJasDbContext _context;

    public DocumentationService(ISkillJasDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(CreateDocumentationDto dto)
    {
        var doc = new Documentation
        {
            Title = dto.Title,
            Description = dto.Description,
            Url = dto.Url
        };

        _context.Documentation.Add(doc);
        await _context.SaveChangesAsync();
        return doc.Id;
    }

    public async Task<List<DocumentationDto>> GetAllAsync()
    {
        return await _context.Documentation
            .Where(d => d.IsActive)
            .OrderByDescending(d => d.CreatedDate)
            .Select(d => new DocumentationDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description,
                Url = d.Url,
                IsActive = d.IsActive,
                CreatedDate = d.CreatedDate
            }).ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, UpdateDocumentationDto dto)
    {
        var doc = await _context.Documentation.FindAsync(id);
        if (doc is null)
            return false;

        doc.Title = dto.Title;
        doc.Description = dto.Description;
        doc.Url = dto.Url;
        doc.IsActive = dto.IsActive;
        doc.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }


}
