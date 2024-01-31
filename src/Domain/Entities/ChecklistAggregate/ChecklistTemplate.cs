
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities;

public class ChecklistTemplate : AuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    public required string ItemTemplateId {get; set;}

    public ItemTemplate ItemTemplate {get; set;} = null!;

    public List<ChecklistTemplateChecklistItem> ChecklistTemplateChecklistItems {get; set;} = new List<ChecklistTemplateChecklistItem>();

    public List<ChecklistItem> ChecklistItems {get; set;} = new List<ChecklistItem>();
}
