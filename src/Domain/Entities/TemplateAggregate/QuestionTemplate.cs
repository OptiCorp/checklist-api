using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplate : Entity
{
    // public Guid ItemTemplateId {get; private set;}
    // public ItemTemplate ItemTemplate {get;} = null!;
    public string Question { get; private set; }

    public void UpdateQuestion(string newQuestion)
    {
        Question = newQuestion;
    }

    public static QuestionTemplate New(string question)
    {
        return new QuestionTemplate()
        {
            Question = question
        };
    }
}
