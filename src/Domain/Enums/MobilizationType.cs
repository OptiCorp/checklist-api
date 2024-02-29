using System.ComponentModel.DataAnnotations;

namespace MobDeMob.Domain.Entities;

public enum MobilizationType
{
    [Display(Name = "Mobilization")]
    Mobilization,
    [Display(Name = "DeMobilization")]
    Demobilization,

}