namespace SkillJas.Application.DTOs;

public class FavoriteDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}
