using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.ChecklistAggregate;


public class ChecklistItem : AuditableEntity
{
    public string ItemId { get; set; }

    public Guid ChecklistId { get; set; }

    //public Checklist Checklist {get; set;}

    public Guid TemplateId { get; set; }

    public ItemTemplate Template { get; set; } = null!;

    public ICollection<ChecklistItemQuestion> Questions { get; set; } = [];

    public ICollection<Punch> Punches { get; set; } = [];


    [NotMapped]
    public int? PunchesCount {get; set;}

    public ChecklistItemStatus Status { get; set; }

    public double CompletionPercentage => GetCompletionPercentage();

    public ChecklistItem(ItemTemplate itemTemplate, Guid checklistId)
    {
        ItemId = itemTemplate.ItemId;
        ChecklistId = checklistId;
        TemplateId = itemTemplate.Id;
    }

    protected ChecklistItem()
    {

    }

    private double GetCompletionPercentage()
    {
        //var questions = ChecklistItems.SelectMany(ci => ci.Questions);
        if (Questions.Count == 0) return 0;
        var questionsApplicable = Questions.Where(q => q.NotApplicable == false);
        if (!questionsApplicable.Any()) return 0;
        
        var completionProgressionDecimal = (double)questionsApplicable.Count(i => i.Checked) / questionsApplicable.Count();
        var completionPercentage = 100 * completionProgressionDecimal;
        return completionPercentage;
    }


}
