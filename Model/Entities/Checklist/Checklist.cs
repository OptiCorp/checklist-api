
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class Checklist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id {get; set;}
    public required string ItemId {get; set;}

    public required Item Item {get; set;}

    public required string ChecklistTemplateId {get; set;}

    public required ChecklistTemplate ChecklistTemplate {get; set;}

    public required string MobilizationId {get; set;}

    public required Mobilization Mobilization {get; set;}

    public ICollection<Punch> Puches {get; set;} = new List<Punch>();
}