using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;
using skillJas.Domain.Entities;
using Microsoft.EntityFrameworkCore;


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
                CreatedDate = DateTime.UtcNow
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
                    IsActive = course.IsActive
                };
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    IsActive = c.IsActive
                })
                .ToListAsync();
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
