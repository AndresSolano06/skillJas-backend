using skillJas.Application.DTOs;

namespace skillJas.Application.Interfaces;

public interface ICourseService
{
    Task<int> CreateAsync(CreateCourseDto dto);
    Task<CourseDto?> GetByIdAsync(int id);
    Task<List<CourseDto>> GetAllAsync();
    Task<bool> PauseAsync(int id);
}
 