using System.ComponentModel.DataAnnotations;

namespace MobDeMob.Domain.Entities.Mobilization;

public enum MobilizationStatus
{
    [Display(Name = "NotReady")]
    NotReady,
    [Display(Name = "Ready")]
    Ready

}