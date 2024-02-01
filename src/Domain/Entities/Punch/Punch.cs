using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class Punch : AuditableEntity
{
    // public string? Description { get; set; }

    // public required string ChecklistChecklistItemId { get; set; }

    // //public ChecklistChecklistItem ChecklistChecklistItem {get; set;} = null!;
    // public string? ChecklistId {get; set;}

    // public Checklist? Checklist {get; set;} = null!;

    // public required string ItemId {get; set;}

    // public Item Item {get; set;} = null!;
    public required string SectionId { get; set; }

    public ChecklistSection Section { get; set; } = null!;

    public required string Title { get; set; }

    public string? Description { get; set; }

    [NotMapped]
    public Item Item => Section.Item;


}