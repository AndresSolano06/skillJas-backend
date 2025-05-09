using SkillJas.Application.DTOs;

namespace SkillJas.Application.Interfaces;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCourseDto dto);
    Task<bool> PauseAsync(int id);
}
