using System.ComponentModel.DataAnnotations;
namespace MobDeMob.Domain.Enums;
public enum ChecklistItemStatus
{
    [Display(Name = "NotStarted")]
    NotStarted,
    [Display(Name = "InProgress")]
    InProgress,
    [Display(Name = "Completed")]
    Completed

}