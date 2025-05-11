using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;
using skillJas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace skillJas.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ISkillJasDbContext _context;

        public CourseService(ISkillJasDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CourseUrl = dto.CourseUrl
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course.Id;
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            return course == null
                ? null
                : new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Category = course.Category,
                    CreatedDate = course.CreatedDate,
                    UpdatedDate = course.UpdatedDate,
                    IsActive = course.IsActive,
                    CourseUrl = course.CourseUrl,
                };
        }

        public async Task<PaginatedResult<CourseDto>> GetActiveCoursesAsync(int page, int pageSize, string? category)
        {
            // Filtrar cursos activos
            var query = _context.Courses.Where(c => c.IsActive);

            // Filtrar por categoría (si aplica)
            if (!string.IsNullOrWhiteSpace(category))
            {
                var normalizedCategory = category.ToLower();
                query = query.Where(c => c.Category.Any(cat => cat.ToLower() == normalizedCategory));
            }

            // Orden por fecha más reciente
            query = query.OrderByDescending(c => c.CreatedDate);

            // Paginación
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    IsActive = c.IsActive,
                    CourseUrl = c.CourseUrl
                })
                .ToListAsync();

            return new PaginatedResult<CourseDto>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<bool> PauseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.IsActive = false;
            course.UpdatedDate = DateTime.UtcNow;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
