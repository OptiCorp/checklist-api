using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Domain.Entities.ChecklistAggregate;

public class ChecklistQuestion : AuditableEntity
{
    public Guid ChecklistId { get; private set; }

    public QuestionTemplate QuestionTemplate { get; set; } = null!;

    public Guid QuestionTemplateId { get; private set; }

    public bool Checked { get; private set; }

    public bool NotApplicable { get; private set; }

    [NotMapped]
    public string Question
    {
        get => QuestionTemplate?.Question ?? string.Empty;
    }

    public ChecklistQuestion(QuestionTemplate questionTemplate, Guid checklistId)
    {
        ChecklistId = checklistId;
        QuestionTemplateId = questionTemplate.Id;
        
    }

    public bool IsQuestionCheckable() => !NotApplicable;

    public void MarkQuestionAsNotApplicable(bool value)
    {
        if (value)
        {
            if (Checked)
            {
                throw new Exception("Tried to mark question as not applicable when it is checked");
            }
            NotApplicable = true;
        }else {
            NotApplicable = false;
        }
    }

    public void MarkQuestionAsCheckedOrUnChecked(bool value)
    {
        if (value)
        {
            if (!IsQuestionCheckable())
            {
                throw new Exception("Tried to mark question as checked when it is not checkable");
            }
            Checked = true;
        }else {
            Checked = false;
        }

    }

    protected ChecklistQuestion()
    {

    }
}
