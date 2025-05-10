using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class User : AuditableEntity
{
    public Guid Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;


    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Progress> Progress { get; set; } = new List<Progress>();
}
