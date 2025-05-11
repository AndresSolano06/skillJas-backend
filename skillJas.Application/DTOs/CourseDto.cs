namespace skillJas.Application.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Category { get; set; } = new(); 
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? CourseUrl { get; set; }

    public bool IsActive { get; set; }
}
