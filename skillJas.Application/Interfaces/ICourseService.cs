using skillJas.Application.DTOs;

namespace skillJas.Application.Interfaces;

public interface ICourseService
{
    Task<int> CreateAsync(CreateCourseDto dto);
    Task<CourseDto?> GetByIdAsync(int id);
    Task<PaginatedResult<CourseDto>> GetCoursesAsync(int page, int pageSize, string? category);
    Task<bool> DisableAsync(int id);
    Task<bool> EnableAsync(int id);
    Task<Dictionary<string, int>> GetNormalizedCategoriesAsync();


}
