using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;
using Domain.Entities;

namespace Domain.Entities.ChecklistAggregate;


public class Checklist : AuditableEntity
{
    //public ChecklistCollection ChecklistCollection {get; set;} = null!
    public Guid ChecklistCollectionId { get; private set; }

    public ItemTemplate ItemTemplate {get; set;} = null!;
    public Guid ItemTemplateId {get; private set;}

    //public Checklist Checklist {get; set;}

    public ICollection<ChecklistQuestion> Questions { get; private set; } = [];

    public ICollection<Punch> Punches { get; set; } = [];


    private int _punchesCount;
    [NotMapped]
    public int PunchesCount
    {
        get => _punchesCount;
        set => _punchesCount = value;
    }

    [NotMapped]
    public string ItemId => ItemTemplate?.ItemId ?? string.Empty; 

    public ChecklistStatus Status { get; private set; }

    public double CompletionPercentage => GetCompletionPercentage();

    public Checklist(ItemTemplate itemTemplate, Guid checklistCollectionId)
    {
        ItemTemplate = itemTemplate;
        ChecklistCollectionId = checklistCollectionId;
        //_itemId = itemTemplate.ItemId;
    }

    public Checklist SetChecklistStatus(ChecklistStatus status)
    {
        Status = status;
        return this;
    }

    public void SetQuestions(IEnumerable<ChecklistQuestion> checklistItemQuestions)
    {
        Questions = new List<ChecklistQuestion>(checklistItemQuestions);
    }

    public void SetPunchesCount(int punchesCount)
    {
        PunchesCount = punchesCount;
    }


    protected Checklist()
    {

    }

    private double GetCompletionPercentage()
    {
        //var questions = ChecklistItems.SelectMany(ci => ci.Questions);
        if (!Questions.Any()) return 0;
        var questionsApplicable = Questions.Where(q => q.NotApplicable == false);
        if (!questionsApplicable.Any()) return 0;

        var completionProgressionDecimal = (double)questionsApplicable.Count(i => i.Checked) / questionsApplicable.Count();
        var completionPercentage = 100 * completionProgressionDecimal;
        return completionPercentage;
    }


}
