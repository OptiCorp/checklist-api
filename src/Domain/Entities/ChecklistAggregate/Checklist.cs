using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class Checklist : AuditableEntity
{

    // [EnumDataType(typeof(ChecklistStatus))]
    // public ChecklistStatus Status {get; set;}
    public Mobilization? Mobilization { get; set; }
    public IList<string> Parts { get; set; } = new List<string>();
    public ICollection<ChecklistItem> ChecklistItems {get; set;} = [];

    public double GetCompletionPercentage()
    {
        var questions = ChecklistItems.SelectMany(ci => ci.Questions);
        if (!questions.Any()) return 0;
        var completionProgressionDecimal = (double)questions.Count(i => i.Checked) / questions.Count();
        var completionPercentage = 100 * completionProgressionDecimal;
        return completionPercentage;
    }

    [NotMapped]
    public int ChecklistCountDone => ChecklistItems.Count(ci => ci.Status == Enums.ChecklistItemStatus.Completed);

    [NotMapped]
    public int ChecklistCount => ChecklistItems.Count;

    // public int GetChecklistCountDont ()
    // {
    //     return ChecklistItems.Count(ci => ci.Status == Enums.ChecklistItemStatus.Completed);
    // }

    // public int GetChecklistCountDo ()
    // {
    //     return ChecklistItems.Count(ci => ci.Status == Enums.ChecklistItemStatus.Completed);
    // }

    //public ICollection<ChecklistSection> ChecklistSections { get; set; } = new List<ChecklistSection>();

    //[NotMapped]
    //public IEnumerable<Punch> Punches => ChecklistSections.SelectMany(section => section.Punches) ?? Enumerable.Empty<Punch>();

    //[NotMapped]
    //public double CompletionPercentage => GetCompletionPercentage();

    //private double GetCompletionPercentage()
    //{
    //    var allSections = ChecklistSections.SelectMany(section => section.GetAllSections());

    //    var completionProgressionDecimal = allSections.Count(i => i.IsCompleted) / allSections.Count();
    //    var completionProgressionPercentage = 100.0 * completionProgressionDecimal;
    //    return completionProgressionPercentage;
    //}


    // public List<Punch> Punch {get; set;} = [];

    // public required string ChecklistTemplateId {get; set;}

    // public ChecklistTemplate ChecklistTemplate {get; set;} = null!;

    // public required string MobilizationId {get; set;}

    // public Mobilization Mobilization {get; set;} = null!;

    // public List<ChecklistChecklistItem> ChecklistChecklistItems {get; set;} = null!;
}
