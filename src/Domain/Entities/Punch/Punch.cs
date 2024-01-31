using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities;
public class Punch: AuditableEntity
{
    public required string Id { get; set; }

    public string? Description { get; set; }

    public required string ChecklistChecklistItemId { get; set; }

    //public ChecklistChecklistItem ChecklistChecklistItem {get; set;} = null!;
    public string? ChecklistId {get; set;}

    public Checklist? Checklist {get; set;} = null!;

    public required string ItemId {get; set;}

    public Item Item {get; set;} = null!;


}