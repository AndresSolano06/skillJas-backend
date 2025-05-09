using SkillJas.Domain.Common;
using System;

namespace SkillJas.Domain.Entities;

public class Course : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Progress> Progress { get; set; } = new List<Progress>();
}
