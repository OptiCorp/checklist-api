using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class ItemTemplate : AuditableEntity
{
    public Item Item { get; set; } = null!;
    public required string ItemId { get; set; }

    // public Guid? ParentItemTemplateId {get; set;}
    // public ItemTemplate? ParentItemTemplate {get; set;}
    // public ICollection<ItemTemplate> Children {get; set;} = [];

    public ICollection<QuestionTemplate> Questions { get; set; } = [];

    public ItemTemplate UpdateQuestions(IEnumerable<string>? questions)
    {
        Questions = questions
                    ?.Select(q => new QuestionTemplate { Question = q })
                    ?.ToList() ?? [];
        return this;
    }
}

