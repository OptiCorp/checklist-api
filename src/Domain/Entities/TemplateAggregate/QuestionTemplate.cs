using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplate : AuditableEntity
{
    // public Guid ItemTemplateId {get; private set;}
    // public ItemTemplate ItemTemplate {get;} = null!;
    public string Question { get; init; } 
}
