using SkillJas.Domain.Common;
using System;

namespace SkillJas.Domain.Entities;

public class User : AuditableEntity
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Progress> Progress { get; set; } = new List<Progress>();
}
