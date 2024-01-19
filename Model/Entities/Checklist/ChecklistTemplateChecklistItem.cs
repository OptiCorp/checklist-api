using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistTemplateChecklistItem //This table is not strictly neccessary
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    public int ChecklistItemId {get; set;}

    public int ChecklistTemplateId {get; set;}

    public ChecklistItem ChecklistItem {get; set;} = null!; //Not neccessary

    public ChecklistTemplate ChecklistTemplate {get; set;} = null!; //Not necceassry



}