
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Enums;

namespace MobDeMob.Domain.Entities;

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