namespace skillJas.Application.DTOs;

public class CreateCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string? CourseUrl { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
