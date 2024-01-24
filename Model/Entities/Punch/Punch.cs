namespace Model.Entities;

public class Punch
{
    public required string Id { get; set; }

    public DateOnly PunchCreated { get; set; }

    public string? Description { get; set; }

    public required string ChecklistChecklistItemId { get; set; }

    //public ChecklistChecklistItem ChecklistChecklistItem {get; set;} = null!;
    public string? ChecklistId {get; set;}

    public Checklist? Checklist {get; set;} = null!;

    public required string ItemId {get; set;}

    public Item Item {get; set;} = null!;


}