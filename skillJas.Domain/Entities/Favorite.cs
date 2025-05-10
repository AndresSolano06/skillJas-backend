using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class Favorite : AuditableEntity
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;


    public Course? Course { get; set; }
}
