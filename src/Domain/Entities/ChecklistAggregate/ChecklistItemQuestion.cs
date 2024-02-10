using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Domain.Entities.ChecklistAggregate;

public class ChecklistItemQuestion : AuditableEntity
{
    public string ChecklistItemId { get; set; }

    public string QuestionTemplateId { get; set; }

    public string Value { get; set; }

    public ChecklistItemQuestion(QuestionTemplate questionTemplate, string checklistItemId)
    {
        ChecklistItemId = checklistItemId;
        QuestionTemplateId = questionTemplate.Id;
    }

    protected ChecklistItemQuestion()
    {

    }
}
