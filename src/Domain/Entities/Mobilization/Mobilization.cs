using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Mobilization.Events;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Domain.Entities;

public class Mobilization : AuditableEntity
{
    public string Title { get; private set; }

    public string Description { get; private set; }

    public MobilizationType Type { get; private set; }

    public MobilizationStatus Status { get; private set; }

    public Guid ChecklistCollectionId { get; private set; }

    public ChecklistCollection ChecklistCollection { get; set; } = null!;

    // [NotMapped]
    // public IList<string> PartIds { 
    //     get => Checklist?.Parts ?? []; 
    // }
    // [NotMapped]
    // public int PartsCount => Checklist.Parts.Count;

    // [NotMapped]
    // public double CompletionPercentage => Checklist.GetCompletionPercentage();

    [NotMapped]
    public int ChecklistCountDone => ChecklistCollection.ChecklistsCountDone;

    [NotMapped]
    public int ChecklistCount => ChecklistCollection.ChecklistsCount;

    // private double GetCompletionPercentage()
    // {
    //     var questions = Checklist.ChecklistItems.SelectMany(ci => ci.Questions);
    //     if (!questions.Any()) return 0;
    //     var completionProgressionDecimal = (double)questions.Count(i => i.Checked) / questions.Count();
    //     var completionPercentage = 100 * completionProgressionDecimal;
    //     return completionPercentage;
    // }
    // public Mobilization(string title, Guid checklistId, string description = "")
    // {
    //     Title = title;
    //     ChecklistId = checklistId;
    //     Description = description ?? string.Empty;
    // }

    public static Mobilization New(string title, MobilizationType mobilizationType, MobilizationStatus mobilizationStatus, Guid checklistCollectionId, string? description = "")
    {
        var newMob = new Mobilization()
        {
            Title = title,
            Type = mobilizationType,
            Status = mobilizationStatus,
            ChecklistCollectionId = checklistCollectionId,
            Description = description ?? string.Empty
        };
        return newMob;
    }

    public Mobilization SetTitle(string title)
    {
        Title = title;
        return this;
    }

    public Mobilization SetDescription(string description = "")
    {
        Description = description;
        return this;
    }

    public Mobilization SetStatus(MobilizationStatus status)
    {
        Status = status;    
        return this;
    }



    //[NotMapped]
    //public IEnumerable<Punch> Punches => Checklist?.Punches ?? Enumerable.Empty<Punch>();

    public void DeleteParts()
    {
        AddDomainEvent(new MobilizationDeleted(Id));
    }
}
