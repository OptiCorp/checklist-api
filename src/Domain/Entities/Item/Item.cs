using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities;

public class Item
{
    public string Id {get; set;}

    public ItemTemplate ItemTemplate {get; private set;} = null!;

    public string ItemTemplateId {get; private set;} 

    public static Item New (string itemTemplateId, string itemId)
    {
        return new Item()
        {
            Id = itemId,
            ItemTemplateId = itemTemplateId
        };
    }
}