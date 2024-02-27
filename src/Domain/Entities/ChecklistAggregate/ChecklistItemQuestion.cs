using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Domain.Entities.ChecklistAggregate;

public class ChecklistItemQuestion : AuditableEntity
{
    public Guid ChecklistItemId { get; set; }

    public Guid QuestionTemplateId { get; set; }

    public bool Checked { get; set; } 

    public bool NotApplicable {get; set;}

    public ChecklistItemQuestion(QuestionTemplate questionTemplate, Guid checklistItemId)
    {
        ChecklistItemId = checklistItemId;
        QuestionTemplateId = questionTemplate.Id;
    }

    public bool IsQuestionCheckable() => !NotApplicable;

    public void MarkQuestionAsChecked()
    {
        if(!IsQuestionCheckable())
        {
            throw new Exception("Tried to mark question as checked when it is not chechable");
        }
        Checked = true;
    }

    protected ChecklistItemQuestion()
    {

    }
}
