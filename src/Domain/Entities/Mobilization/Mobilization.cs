using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Mobilization.Events;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Domain.Entities;

public class Mobilization : AuditableEntity
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public MobilizationType Type { get; set; }

    public MobilizationStatus Status { get; set; }

    public Guid ChecklistId { get; set; }

    public Checklist Checklist { get; set; } = null!;

    // [NotMapped]
    // public IList<string> PartIds { 
    //     get => Checklist?.Parts ?? []; 
    // }
    [NotMapped]
    public int PartsCount => Checklist.Parts.Count;

    // [NotMapped]
    // public double CompletionPercentage => Checklist.GetCompletionPercentage();

    [NotMapped]
    public int ChecklistCountDone => Checklist.ChecklistCountDone;

    [NotMapped]
    public int ChecklistCount => Checklist.ChecklistCount;

    // private double GetCompletionPercentage()
    // {
    //     var questions = Checklist.ChecklistItems.SelectMany(ci => ci.Questions);
    //     if (!questions.Any()) return 0;
    //     var completionProgressionDecimal = (double)questions.Count(i => i.Checked) / questions.Count();
    //     var completionPercentage = 100 * completionProgressionDecimal;
    //     return completionPercentage;
    // }


    //[NotMapped]
    //public IEnumerable<Punch> Punches => Checklist?.Punches ?? Enumerable.Empty<Punch>();

    public void DeleteParts()
    {
        AddDomainEvent(new MobilizationDeleted(Id));
    }
}
