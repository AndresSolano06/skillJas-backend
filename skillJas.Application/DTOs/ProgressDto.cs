namespace SkillJas.Application.DTOs;

public class ProgressDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = default!;
    public int Percentage { get; set; }
    public DateTime CreatedDate { get; set; }
}
