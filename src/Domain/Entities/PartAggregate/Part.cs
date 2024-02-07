using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Domain.ItemAggregate;

public abstract class Part : AuditableEntity
{
    public PartType Type { get; private set; }
    public required string WpId { get; set; }

    public required string SerialNumber { get; set; }

    public required string Name { get; set; }

    public string? ChecklistId {get; set;} //foreign key to checklist
    public string? PartTemplateId { get; set; } //foreign key to partTemplate

    public string? PartParentId {get; set;} //foreign key to parent part

    public Part ParentPart {get; set;} //navigation to parent

    public PartTemplate PartTemplate { get; set; } = null!;



    public ICollection<Part> Children { get; set; } = [];

    [NotMapped]
    public bool hasChecklistTemplate {get; set;}

    public Part(PartType partType)
    {
        Type = partType;
    }


    public virtual void AddChild(Part child) //TODO: fix the commented code
    {
        var isChildNotContainableInParent = Type > child.Type;
        if (isChildNotContainableInParent)
        {
            throw new InvalidOperationException($"A {Type} cannot contain an item of type {child.Type}");
        }

        var isChildAlreadyAdded = Children.Any(existingChild => existingChild.Id == child.Id);
        if (isChildAlreadyAdded)
        {
            throw new InvalidOperationException($"Cannot add the same item twice - Item id: {child.Id}");
        }

        Children.Add(child);

        AddDomainEvent(new PartChildAdded { ChildId = child.Id, ParentId = Id });
    }

    public virtual void RemoveChild(Part child)
    {
        var isChildMissingFromParent = !Children.Any(existingChild => existingChild.Id == child.Id);
        if (isChildMissingFromParent)
        {
            throw new InvalidOperationException($"Item {child.Id} could not be removed, because it's not a child of the Item");
        }

        Children.Remove(child);

        AddDomainEvent(new PartChildRemoved { ChildId = child.Id, ParentId = Id });
    }
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

