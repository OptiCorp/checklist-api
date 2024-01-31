using System.ComponentModel.DataAnnotations;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities;

public enum MobilizationType
{
    [Display(Name = "Mobilization")]
    Mobilization,
    [Display(Name = "DeMobilization")]
    DeMobilization,

}
public class Mobilization : AuditableEntity
{
    public required string Id {get; set;}

    public required string Title {get; set;}

    public string? Description {get; set;}

    public MobilizationType MobilizationType {get; set;}

    // public List<Item> Items {get; set;} = new List<Item>();

    public List<ItemMobilization> ItemMobilizations {get; set;} = new List<ItemMobilization>();
    
}