
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class Checklist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    // [EnumDataType(typeof(ChecklistStatus))]
    // public ChecklistStatus Status {get; set;}
    public required string ItemId {get; set;}

    public Item Item {get; set;} = null!; //TODO: 

    public List<Punch> Punch {get; set;} = [];

    public required string ChecklistTemplateId {get; set;}

    public ChecklistTemplate ChecklistTemplate {get; set;} = null!;

    public required string MobilizationId {get; set;}

    public Mobilization Mobilization {get; set;} = null!;

    public List<ChecklistChecklistItem> ChecklistChecklistItems {get; set;} = null!;
}