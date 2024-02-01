using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Domain.Entities.ItemAggregate;

public class ItemTemplate : AuditableEntity
{
    public required string Name { get; set; }

    public required string Type { get; set; }

    public string? Revision { get; set; }

    public string? Description { get; set; }

    public ChecklistSectionTemplate? ItemCheckListTemplate { get; set; }

}

