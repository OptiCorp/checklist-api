using System.ComponentModel.DataAnnotations;

namespace MobDeMob.Domain.Entities;

public enum MobilizationStatus
{
    [Display(Name = "NotReady")]
    NotReady,
    [Display(Name = "Ready")]
    Ready

}