using System.ComponentModel.DataAnnotations;

namespace MobDeMob.Domain.Entities.Mobilization;

public enum MobilizationType
{
    [Display(Name = "Mobilization")]
    Mobilization,
    [Display(Name = "DeMobilization")]
    DeMobilization,

}