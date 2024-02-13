using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Domain.Entities.ChecklistAggregate;

public class ChecklistItemQuestion : AuditableEntity
{
    public Guid ChecklistItemId { get; set; }

    public Guid QuestionTemplateId { get; set; }

    public string? Value { get; set; }

    public ChecklistItemQuestion(QuestionTemplate questionTemplate, Guid checklistItemId)
    {
        ChecklistItemId = checklistItemId;
        QuestionTemplateId = questionTemplate.Id;
    }

    protected ChecklistItemQuestion()
    {

    }
}
