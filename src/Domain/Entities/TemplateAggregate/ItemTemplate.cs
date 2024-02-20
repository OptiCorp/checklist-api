using System.Text.Json.Serialization;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class ItemTemplate : AuditableEntity
{
    public required string ItemId { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }

    public string? Revision { get; set; }

    public string? Description { get; set; }

    // public Guid? ParentItemTemplateId {get; set;}
    // public ItemTemplate? ParentItemTemplate {get; set;}
    // public ICollection<ItemTemplate> Children {get; set;} = [];

    public ICollection<QuestionTemplate> Questions { get; set; } = [];
}

