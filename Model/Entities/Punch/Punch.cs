namespace Model.Entities;

public class Punch 
{
    public int Id {get; set;}

    public DateOnly PunchCreated {get; set;}

    public string? Description {get; set;}

    public int ChecklistChecklistItemId {get; set;}

    public ChecklistChecklistItem ChecklistChecklistItem {get; set;} = null!;
}