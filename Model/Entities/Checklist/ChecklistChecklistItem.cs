
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

public class ChecklistChecklistItem //checklistItemWithStatus
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id { get; set; }

    [EnumDataType(typeof(ChecklistStatus))]
    public ChecklistStatus Status { get; set; }

    public required string ChecklistId { get; set; }

    public Checklist Checklist { get; set; } = null!;

    public required string ChecklistItemId { get; set; }

    public ChecklistItem ChecklistItem { get; set; } = null!;

}