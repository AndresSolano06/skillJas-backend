using SkillJas.Domain.Common;

namespace SkillJas.Domain.Entities;

public class Favorite : AuditableEntity
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public int CourseId { get; set; }

    public User User { get; set; } = default!;
    public Course Course { get; set; } = default!;
}
