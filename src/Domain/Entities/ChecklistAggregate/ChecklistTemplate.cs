
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities;

public class ChecklistTemplate : AuditableEntity
{
    public string ItemTemplateId { get; set; }

    public ItemTemplate ItemTemplate { get; set; } = null!;
    public ICollection<QuestionTemplate> Questions { get; private set; } = [];

    public static ChecklistTemplate New(string itemTemplateId, ICollection<QuestionTemplate>? questions = null) 
    {
        return new ChecklistTemplate()
        {
            ItemTemplateId = itemTemplateId,
            Questions = questions ?? [],
        };
    }

    public ChecklistTemplate SetQuestions(ICollection<QuestionTemplate> questionTemplates)
    {
        Questions = questionTemplates;
        return this;
    }

    public void AddQuestionTemplate(QuestionTemplate questionTemplate)
    {
        Questions.Add(questionTemplate);
    }
}