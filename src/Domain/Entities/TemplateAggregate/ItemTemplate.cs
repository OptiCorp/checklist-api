using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class ItemTemplate : AuditableEntity
{
    public string ItemId { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }

    public string? Revision { get; set; }

    public string? Description { get; set; }

    public ICollection<QuestionTemplate> Questions { get; set; } = [];
}

