using skillJas.Domain.Common;
using skillJas.Domain.Entities;

public class Favorite : AuditableEntity
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public string UserId { get; set; } = default!;
    public User User { get; set; } = null!;
}
