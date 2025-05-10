namespace skillJas.Application.DTOs;

public class CreateCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
