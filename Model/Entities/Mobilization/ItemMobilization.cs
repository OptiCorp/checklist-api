namespace Model.Entities;

public class ItemMobilization
{
    public int Id {get; set;}

    public int MobilizationId {get; set;}

    public int ItemId {get; set;}

    public Mobilization Mobilization {get; set;} = null!;

    public Item Item {get; set;} = null!;

}