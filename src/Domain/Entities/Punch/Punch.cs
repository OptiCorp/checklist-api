using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Common;

namespace Domain.Entities;

public class Punch : AuditableEntity 
{
    // public string? Description { get; set; }

    // public required string ChecklistChecklistItemId { get; set; }

    // //public ChecklistChecklistItem ChecklistChecklistItem {get; set;} = null!;
    // public string? ChecklistId {get; set;}

    // public Checklist? Checklist {get; set;} = null!;

    // public required string ItemId {get; set;}

    // public Item Item {get; set;} = null!;
    public Guid ChecklistId { get; private set; }

    public Checklist Checklist {get; set;} = null!;

    [NotMapped]
    public string? ItemId => Checklist.ItemId;

    //public ChecklistSection Section { get; set; } = null!;
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;


    public ICollection<PunchFile> PunchFiles { get; private set; } = [];

    [NotMapped]
    public string? SasToken {get; private set;}


    public Punch SetTitle(string title)
    {
        Title = title;
        return this;
    }

    public Punch SetDescription(string description = "")
    {
        Description = description;
        return this;
    }

    public Punch SetSasToken(string sasToken)
    {
        SasToken = sasToken;
        return this;
    }

    public static Punch New(string title, Guid checklistId, string description = "")
    {
        var newPunch = new Punch(){
            Title = title,
            Description = description ?? string.Empty,
            ChecklistId = checklistId
        };
        return newPunch;
    }

    //[NotMapped]
    //public Part Part => Section.Part;

    //[NotMapped]
    //public string CheckListId => Section.Part.ChecklistId;
    // consider renaming to just CheckListId, since there is only one (correct me if I'm assumign wrong)
}