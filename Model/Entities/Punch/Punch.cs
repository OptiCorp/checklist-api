namespace Model.Entities;

public class Punch 
{
    public required string Id {get; set;}

    public DateOnly PunchCreated {get; set;}

    public string? Description {get; set;}

    public required string ChecklistId {get; set;}

    public Checklist Checklist {get; set;} = null!;
}