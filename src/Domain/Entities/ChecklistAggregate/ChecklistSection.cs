using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Entities.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSection
{
    public required string ChecklistSectionTemplateId { get; set; }

    public required ChecklistSectionTemplate ChecklistSectionTemplate { get; set; }

    // the question (and some other fields) can be fetched from the template

    public required string ItemId { get; set; }

    public required Item Item { get; set; }

    // the name of the item (or other fields) can be fetched from the item

    public IEnumerable<ChecklistSection> SubSections { get; set; } = [];

    public IEnumerable<Punch> Punches { get; set; } = [];

    public bool IsValidated { get; set; }

    [NotMapped]
    public bool HasPunches => Punches?.Any() ?? false;

    [NotMapped]
    public bool IsCompleted => IsValidated && !HasPunches;

    public IEnumerable<ChecklistSection> GetAllSections() => new List<ChecklistSection> { this }.Concat(SubSections.SelectMany(section => GetAllSections()));

}