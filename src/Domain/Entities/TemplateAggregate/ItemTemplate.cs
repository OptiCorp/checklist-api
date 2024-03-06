using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class ItemTemplate : AuditableEntity
{
    public Item Item { get; set; } = null!;
    public string ItemId { get; private set; }

    // public Guid? ParentItemTemplateId {get; set;}
    // public ItemTemplate? ParentItemTemplate {get; set;}
    // public ICollection<ItemTemplate> Children {get; set;} = [];
    public ItemTemplate(string itemId)
    {
        ItemId = itemId;
    }


    public ICollection<QuestionTemplate> Questions { get; set; } = [];

    public ItemTemplate UpdateQuestions(IEnumerable<string>? questions)
    {
        Questions = questions
                    ?.Select(q => new QuestionTemplate { Question = q }) 
                    ?.ToList() ?? [];
        return this;
    }
}

