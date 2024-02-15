using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.ChecklistAggregate;


public class ChecklistItem : AuditableEntity
{
    public string ItemId { get; set; }

    public Guid ChecklistId { get; set; }

    public Checklist Checklist {get; set;}

    public Guid TemplateId { get; set; }

    public ItemTemplate Template { get; set; }

    public ICollection<ChecklistItemQuestion> Questions { get; set; } = [];

    public ICollection<Punch> Punches {get; set;} = [];

    public ChecklistItemStatus Status {get; set;}

    public ChecklistItem(ItemTemplate itemTemplate, Guid checklistId)
    {
        ItemId = itemTemplate.ItemId;
        ChecklistId = checklistId;
        TemplateId = itemTemplate.Id;
    }

    protected ChecklistItem()
    {

    }
}
