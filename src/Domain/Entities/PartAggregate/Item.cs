namespace MobDeMob.Domain.ItemAggregate;

public class Item : Part
{
    public Item() : base(PartType.Item)
    {
        
    }

    public override void AddChild(Part child)
    {
        throw new InvalidOperationException($"An item cannot add child");
    }

    public override void RemoveChild(Part child)
    {
        throw new InvalidOperationException($"An item cannot have any childs");
    }
}