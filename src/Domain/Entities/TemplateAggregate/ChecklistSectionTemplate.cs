
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSectionTemplate : AuditableEntity
{
    public required string ChecklistQuestion { get; set; }

    public string? ParentChecklistSectionTemplateId {get; set;}

    public ChecklistSectionTemplate ParentChecklistSectionTemplate {get; set;} = null!;

    public ICollection<ChecklistSectionTemplate> SubSections { get; set; } = [];

    public bool HasSubSections => SubSections?.Count > 0;
}