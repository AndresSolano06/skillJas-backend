using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class Progress : AuditableEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int CourseId { get; set; }
    public int Percentage { get; set; } = 0;

    public Course? Course { get; set; }
}
