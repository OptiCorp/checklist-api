using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class Checklist : AuditableEntity
{

    // [EnumDataType(typeof(ChecklistStatus))]
    // public ChecklistStatus Status {get; set;}
    public Mobilization? Mobilization { get; set; }
    public ICollection<Part> Parts { get; set; } = [];// if parts are on the inventory app, we should turn this into just the ids (ICollection<string>)

    public ICollection<ChecklistSection> ChecklistSections { get; set; } = [];

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
