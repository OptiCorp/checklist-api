
namespace MobDeMob.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateOnly Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}