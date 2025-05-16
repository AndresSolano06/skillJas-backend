using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class User : AuditableEntity
{
    public string Id { get; set; } = default!; 

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
