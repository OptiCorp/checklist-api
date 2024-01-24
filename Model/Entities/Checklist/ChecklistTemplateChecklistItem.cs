using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistTemplateChecklistItem //This table is not strictly neccessary
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    public required string ChecklistItemId {get; set;}

    public required string ChecklistTemplateId {get; set;}

    public ChecklistItem ChecklistItem {get; set;} = null!; //Can be removed

    public ChecklistTemplate ChecklistTemplate {get; set;} = null!; //Can be removed
}