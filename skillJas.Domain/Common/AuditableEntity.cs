namespace SkillJas.Domain.Common;

public abstract class AuditableEntity
{
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
