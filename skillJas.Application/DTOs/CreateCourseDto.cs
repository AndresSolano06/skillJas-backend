namespace SkillJas.Application.DTOs;

public class CreateCourseDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
}
