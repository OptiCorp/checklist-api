﻿using MobDeMob.Domain.Common;

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
    public string SectionId { get; set; }

    //public ChecklistSection Section { get; set; } = null!;

    public required string Title { get; set; }

    public string? Description { get; set; }

    public Uri? ImageBlobUri { get; set; }

    //[NotMapped]
    //public Part Part => Section.Part;

    //[NotMapped]
    //public string CheckListId => Section.Part.ChecklistId;
    // consider renaming to just CheckListId, since there is only one (correct me if I'm assumign wrong)
}
