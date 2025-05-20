using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;
using skillJas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using skillJas.Domain.Common;


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

        public async Task<PaginatedResult<CourseDto>> GetCoursesAsync(int page, int pageSize, string? category)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                var normalizedCategory = category.ToLower();
                query = query
                    .AsEnumerable()
                    .Where(c => c.Category.Any(cat => cat.ToLower() == normalizedCategory))
                    .AsQueryable();
            }

            query = query.OrderByDescending(c => c.CreatedDate);

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
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
                .ToList();

            return new PaginatedResult<CourseDto>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }


        public async Task<bool> DisableAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.IsActive = false;
            course.UpdatedDate = DateTime.UtcNow;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EnableAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.IsActive = true;
            course.UpdatedDate = DateTime.UtcNow;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<string, int>> GetNormalizedCategoriesAsync()
        {
            var rawCategories = _context.Courses
                .AsEnumerable()
                .SelectMany(c => c.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .ToList();

            var normalized = rawCategories
                .Select(CategoryNormalizer.Normalize)
                .GroupBy(n => n)
                .ToDictionary(g => g.Key, g => g.Count());

            return normalized;

        }


    }
}
