using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.ChecklistAggregate;

public class ChecklistItem : AuditableEntity
{
    public string ItemId { get; set; }

    public string ChecklistId { get; set; }

    public string TemplateId { get; set; }

    public ItemTemplate Template { get; set; }

    public ICollection<ChecklistItemQuestion> Questions { get; set; } = [];

    public ChecklistItem(ItemTemplate itemTemplate, string checklistId)
    {
        ItemId = itemTemplate.ItemId;
        ChecklistId = checklistId;
        TemplateId = itemTemplate.Id;
    }

    protected ChecklistItem()
    {

    }
}
