namespace skillJas.Application.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}
