using System.ComponentModel.DataAnnotations;
namespace MobDeMob.Domain.Enums;
public enum ChecklistStatus
{
    [Display(Name = "NotStarted")]
    NotStarted,
    [Display(Name = "Completed")]
    Completed,
    [Display(Name = "InProgress")]
    InProgress,
}