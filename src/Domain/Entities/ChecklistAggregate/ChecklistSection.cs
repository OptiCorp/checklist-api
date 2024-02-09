using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistSection : AuditableEntity
{
    public string ChecklistSectionTemplateId { get; set; }

    public ChecklistSectionTemplate ChecklistSectionTemplate { get; set; } = null!;

    public string PartId { get; set; }

    public Part Part { get; set; } = null!;

    public string ChecklistId { get; set; }

    public Checklist Checklist { get; set; } = null!;

    //public string? ChecklistSectionId {get; set;} //foreign key to parent

    public ChecklistSection? ParentChecklistSection { get; set; }

    public ICollection<ChecklistSection> SubSections { get; set; } = [];

    public ICollection<Punch> Punches { get; set; } = [];

    public bool IsValidated { get; set; }

    [NotMapped]
    public bool HasPunches => Punches?.Any() ?? false;

    [NotMapped]
    public bool IsCompleted => IsValidated && !HasPunches;

    public IEnumerable<ChecklistSection> GetAllSections() => new List<ChecklistSection> { this }.Concat(SubSections.SelectMany(section => GetAllSections()));

}
