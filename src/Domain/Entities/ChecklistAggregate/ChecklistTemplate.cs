
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities;

public class ChecklistTemplate : AuditableEntity
{
    public string ItemTemplateId { get; private set; }

    public ItemTemplate ItemTemplate { get; set; } = null!;
    public ICollection<QuestionTemplate> Questions { get; set; } = [];

    public static ChecklistTemplate New(string itemTemplateId, ICollection<QuestionTemplate> questions)
    {
        return new ChecklistTemplate()
        {
            ItemTemplateId = itemTemplateId,
            Questions = questions,
        };
    }

    public void AddQuestionTemplate(QuestionTemplate questionTemplate)
    {
        Questions.Add(questionTemplate);
    }
}