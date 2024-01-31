namespace MobDeMob.Domain.Entities;

public class ItemMobilization
{
    public required string Id {get; set;}

    public required string MobilizationId {get; set;}

    public required string ItemId {get; set;}

    public Mobilization Mobilization {get; set;} = null!;

    public Item Item {get; set;} = null!;

}