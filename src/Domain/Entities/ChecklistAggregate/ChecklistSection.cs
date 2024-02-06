using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSection : AuditableEntity
{
    public string? ChecklistSectionTemplateId { get; set; }

    public ChecklistSectionTemplate ChecklistSectionTemplate { get; set; } = null!;

    // the question (and some other fields) can be fetched from the template
    
    public string? PartId { get; set; }

    public Part Part { get; set; } = null!;

    // public string? ChecklistSectionId {get; set;}

    public string? ChecklistId {get; set;}

    public Checklist Checklist {get; set;} = null!;

    // the name of the item (or other fields) can be fetched from the Part
    public string? ChecklistSectionId {get; set;} //foreign key to parent

    public ICollection<ChecklistSection> SubSections { get; set; } = [];

    public IEnumerable<Punch> Punches { get; set; } = [];

    public bool IsValidated { get; set; }

    [NotMapped]
    public bool HasPunches => Punches?.Any() ?? false;

    [NotMapped]
    public bool IsCompleted => IsValidated && !HasPunches;

    public IEnumerable<ChecklistSection> GetAllSections() => new List<ChecklistSection> { this }.Concat(SubSections.SelectMany(section => GetAllSections()));

}