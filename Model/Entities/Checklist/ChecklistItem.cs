
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    [MaxLength(150)]
    public required string Question {get; set;} 

    public List<ChecklistTemplateChecklistItem> ChecklistTemplateChecklistItems {get; set;} = new List<ChecklistTemplateChecklistItem>();

    public List<ChecklistTemplate> ChecklistTemplates {get; set;} = new List<ChecklistTemplate>();
}