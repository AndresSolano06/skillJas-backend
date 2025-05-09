using SkillJas.Application.DTOs;

namespace SkillJas.Application.Interfaces;

public interface IProgressService
{
    Task<List<ProgressDto>> GetProgressByUserAsync(string userId);
    Task<int> SaveProgressAsync(string userId, SaveProgressDto dto);
}
