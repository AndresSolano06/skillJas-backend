using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class Course : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string? CourseUrl { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Progress> Progress { get; set; } = new List<Progress>();
}
