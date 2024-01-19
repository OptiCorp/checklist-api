using System.ComponentModel.DataAnnotations;

namespace Model.Entities;


public enum MobilizationType
{
    [Display(Name = "Mobilization")]
    Mobilization,
    [Display(Name = "DeMobilization")]
    DeMobilization,

}
public class Mobilization 
{
    public int Id {get; set;}

    public DateOnly CreatedDate {get; set;}

    public required string Title {get; set;}

    public string? Description {get; set;}

    [EnumDataType(typeof(UserStatus))]

    public MobilizationType MobilizationType {get; set;}

    public List<Item> Items {get; set;} = new List<Item>();

    public List<ItemMobilization> ItemMobilizations {get; set;} = new List<ItemMobilization>();
    
}