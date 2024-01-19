
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistChecklistItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    public int ChecklistId {get; set;}

    public Checklist Checklist {get; set;} = null!;

    public int ChecklistItemId {get; set;}

    public ChecklistItem ChecklistItem {get; set;} = null!;

    public Punch? Puch {get; set;}


    //TODO: add additional props
}