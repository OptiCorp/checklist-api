
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSectionTemplate : AuditableEntity
{
    public required string ChecklistQuestion { get; set; }

    public IEnumerable<ChecklistSectionTemplate> SubSections { get; set; } = [];

    public bool HasSubSections => SubSections?.Any() ?? false;
}