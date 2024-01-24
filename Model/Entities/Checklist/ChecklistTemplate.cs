
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistTemplate
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    public required string ItemTemplateId {get; set;}

    public ItemTemplate ItemTemplate {get; set;} = null!;

    public List<ChecklistTemplateChecklistItem> ChecklistTemplateChecklistItems {get; set;} = new List<ChecklistTemplateChecklistItem>();

    public List<ChecklistItem> ChecklistItems {get; set;} = new List<ChecklistItem>();
}
