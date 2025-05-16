using skillJas.Domain.Common;

namespace skillJas.Domain.Entities;

public class Documentation : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!; 
    public string Url { get; set; } = null!;
}
