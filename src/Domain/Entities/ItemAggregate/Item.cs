using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities.ItemAggregate;

public abstract class Item : AuditableEntity
{
    public abstract ItemType Type { get; }
    public required string WpId { get; set; }

    public required string SerialNumber { get; set; }

    public required string Name { get; set; }

    public required string ItemTemplateId { get; set; }

    public ItemTemplate ItemTemplate { get; set; } = null!;

    public ICollection<Item> Children { get; set; } = [];


    // public void AddChild(Item child) //TODO: fix the commented code
    // {
    //     var isChildNotContainableInParent = Type >= child.Type;
    //     if (isChildNotContainableInParent)
    //     {
    //         throw new InvalidOperationException($"A {Type} cannot contain an item of type {child.Type}");
    //     }

    //     var isChildAlreadyAdded = Children.Any(existingChild => existingChild.Id == child.Id);
    //     if (isChildAlreadyAdded)
    //     {
    //         throw new InvalidOperationException($"Cannot add the same item twice - Item id: {child.Id}");
    //     }

    //     Children.Add(child);

    //     AddDomainEvent(new ItemChildAdded { ChildId = child.Id, ParentId = Id });
    // }

    // public void RemoveChild(Item child)
    // {
    //     var isChildMissingFromParent = !Children.Any(existingChild => existingChild.Id == child.Id);
    //     if (isChildMissingFromParent)
    //     {
    //         throw new InvalidOperationException($"Item {child.Id} could not be removed, because it's not a children of the Item");
    //     }

    //     Children.Remove(child);

    //     AddDomainEvent(new ItemChildRemoved { ChildId = child.Id, ParentId = Id });
    // }
    // [MaxLength(300)]
    // public string? Comment { get; set; }

    // public ICollection<Item>? Children { get; set; }

    // public required string ItemTemplateId { get; set; }
    // public ItemTemplate ItemTemplate { get; set; } = null!;

    // //public ICollection<Document>? Documents { get; set; }
    // public required string ParentId { get; set; }
    // public Item? Parent { get; set; }

    // public List<Mobilization> Mobilizations {get; set;} = [];

    // public List<ItemMobilization> ItemMobilizations {get; set;} = [];

    // public List<Punch> Punches {get; set;} = [];

}

