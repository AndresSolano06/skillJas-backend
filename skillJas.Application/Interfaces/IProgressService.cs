using skillJas.Application.DTOs;

namespace skillJas.Application.Interfaces;

public interface IProgressService
{
    Task<List<ProgressDto>> GetProgressByUserAsync(string userId);
    Task<int> SaveProgressAsync(string userId, SaveProgressDto dto);
}
