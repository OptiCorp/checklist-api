
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public enum ChecklistStatus
{
    [Display(Name = "NotStarted")]
    NotStarted,
    [Display(Name = "Completed")]
    Completed,
    [Display(Name = "InProgress")]
    InProgress,
}
public class Checklist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    // [EnumDataType(typeof(ChecklistStatus))]
    // public ChecklistStatus Status {get; set;}
    public int ItemId {get; set;}

    public Item Item {get; set;} = null!; //TODO: 

    public int ChecklistTemplateId {get; set;}

    public ChecklistTemplate ChecklistTemplate {get; set;} = null!;

    public int MobilizationId {get; set;}

    public Mobilization Mobilization {get; set;} = null!;

    public List<ChecklistChecklistItem> ChecklistChecklistItems {get; set;} = null!;
}