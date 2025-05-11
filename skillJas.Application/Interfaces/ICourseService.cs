using skillJas.Application.DTOs;

namespace skillJas.Application.Interfaces;

public interface ICourseService
{
    Task<int> CreateAsync(CreateCourseDto dto);
    Task<CourseDto?> GetByIdAsync(int id);
    Task<bool> PauseAsync(int id);
    Task<PaginatedResult<CourseDto>> GetActiveCoursesAsync(int page, int pageSize, string? category);

}
