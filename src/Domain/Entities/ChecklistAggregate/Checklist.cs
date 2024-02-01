
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class Checklist : AuditableEntity
{

    // [EnumDataType(typeof(ChecklistStatus))]
    // public ChecklistStatus Status {get; set;}

    public IEnumerable<Item> Items { get; set; } = [];

    public IEnumerable<ChecklistSection> ChecklistSections { get; set; } = [];

    [NotMapped]
    public IEnumerable<Punch> Punches => ChecklistSections.SelectMany(section => section.Punches) ?? Enumerable.Empty<Punch>();

    [NotMapped]
    public double CompletionPercentage => GetCompletionPercentage();

    private double GetCompletionPercentage()
    {
        var allSections = ChecklistSections.SelectMany(section => section.GetAllSections());

        var completionProgressionDecimal = allSections.Count(i => i.IsCompleted) / allSections.Count();
        var completionProgressionPercentage = 100.0 * completionProgressionDecimal;
        return completionProgressionPercentage;
    }


    // public List<Punch> Punch {get; set;} = [];

    // public required string ChecklistTemplateId {get; set;}

    // public ChecklistTemplate ChecklistTemplate {get; set;} = null!;

    // public required string MobilizationId {get; set;}

    // public Mobilization Mobilization {get; set;} = null!;

    // public List<ChecklistChecklistItem> ChecklistChecklistItems {get; set;} = null!;
}