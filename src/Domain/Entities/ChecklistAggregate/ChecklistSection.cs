using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSection : AuditableEntity
{
    public required string ChecklistSectionTemplateId { get; set; }

    public required ChecklistSectionTemplate ChecklistSectionTemplate { get; set; }

    // the question (and some other fields) can be fetched from the template
    
    public required string PartId { get; set; }

    public required Part Part { get; set; }

    public string? ChecklistSectionId {get; set;}

    public required string ChecklistId {get; set;}

    // the name of the item (or other fields) can be fetched from the Part

    public IEnumerable<ChecklistSection> SubSections { get; set; } = [];

    public IEnumerable<Punch> Punches { get; set; } = [];

    public bool IsValidated { get; set; }

    [NotMapped]
    public bool HasPunches => Punches?.Any() ?? false;

    [NotMapped]
    public bool IsCompleted => IsValidated && !HasPunches;

    public IEnumerable<ChecklistSection> GetAllSections() => new List<ChecklistSection> { this }.Concat(SubSections.SelectMany(section => GetAllSections()));

}